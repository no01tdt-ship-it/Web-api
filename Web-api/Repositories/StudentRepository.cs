using Microsoft.EntityFrameworkCore;
using System.Linq;
using Web_api.Constants;
using Web_api.Data;
using Web_api.Extensions;
using Web_api.Interfaces.IRepositories;
using Web_api.Models;
using Web_api.ViewModels.Common;
using Web_api.ViewModels.Student;

namespace Web_api.Repositories
{
    public class StudentRepository : IStudentRepository
    {

        private readonly ApplicationDb _context;

        public StudentRepository(ApplicationDb context)
        {
            _context = context;
        }

        public async Task AddAsync(Student student)
        {
            await _context.Students.AddAsync(student);
        }

        public async Task<bool> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<List<Student>> GetAllAsync()
        {
            return await _context.Students.Include(s => s.SchoolClass).AsNoTracking().ToListAsync();
        }

        public async Task<Student?> GetByIdAsync(long id)
        {
            return await _context.Students.Include(s => s.SchoolClass).AsNoTracking().FirstOrDefaultAsync(s => s.Id == id);
        }

        public async Task<DTResult<ListStudentViewModel>> ListServerSide(StudentViewModelParameters parameters)
        {
            // 1. Khởi tạo câu truy vấn gốc không theo dõi (AsNoTracking để tăng tốc độ truy vấn)
            var query = _context.Students.AsNoTracking();

            // 2. Đếm tổng số bản ghi thực tế trong DB khi chưa áp dụng lọc tìm kiếm
            var totalRecords = await query.CountAsync();

            // 3. Lọc tìm kiếm chung (Thay thế parameters.Search?.Value bằng parameters.SearchAll)
            var searchAll = parameters.SearchText?.Trim() ?? parameters.SearchAll?.Trim() ?? "";
            if (!string.IsNullOrEmpty(searchAll))
            {
                var searchLower = searchAll.ToLower();
                query = query.Where(s =>
                    s.Name.ToLower().Contains(searchLower) ||
                    s.Email.ToLower().Contains(searchLower) ||
                    s.PhoneNumber.ToLower().Contains(searchLower) ||
                    s.Province.ToLower().Contains(searchLower) ||
                    s.CitizenId.ToLower().Contains(searchLower) ||
                    s.SchoolClass != null && s.SchoolClass.Name.ToLower().Contains(searchLower)
                );
            }

            // 4. Đếm số bản ghi thỏa mãn sau khi đã tìm kiếm để DataTables phân trang chính xác
            var filteredRecords = await query.CountAsync();

            // 5. Sử dụng hàm sắp xếp động vừa viết ở Bước 1
            var orderedQuery = query.OrderByDynamic(parameters.SortColumn, parameters.SortDirection);

            // Nếu client không truyền cột để sắp xếp, mặc định sắp xếp theo Id giảm dần (học sinh mới nhất lên đầu)
            if (string.IsNullOrWhiteSpace(parameters.SortColumn))
            {
                orderedQuery = query.OrderByDescending(s => s.Id);
            }

            // 6. Thực thi truy vấn kết hợp: Phân trang (Skip, Take) + Ánh xạ (Select) trực tiếp sang ViewModel
            var listData = await orderedQuery
                .Skip(parameters.Start)
                .Take(parameters.Length)
                .Select(s => new ListStudentViewModel
                {
                    Id = s.Id,
                    Name = s.Name,
                    ClassName = s.SchoolClass != null ? s.SchoolClass.Name : "Chưa xếp lớp",
                    CitizenId = s.CitizenId,
                    PhoneNumber = s.PhoneNumber,
                    // Ánh xạ kiểu bool Gender sang string tương ứng để hiển thị
                    GenderText = s.Gender == GenderConstant.Nam ? "Nam" :
                                 s.Gender == GenderConstant.Nu ? "Nữ" :
                                 s.Gender == GenderConstant.Khac ? "Khác" : "Chưa chọn",
                    Province = s.Province,
                    Email = s.Email,
                    IsRetained = s.IsRetained
                })
                .ToListAsync();

            // 7. Trả về đúng định dạng hộp kết quả DTResult viết hoa theo đúng thuộc tính của Class C#
            return new DTResult<ListStudentViewModel>
            {
                Draw = parameters.Draw,
                RecordsTotal = totalRecords,
                RecordsFiltered = filteredRecords,
                Data = listData
            };
        }


        public async Task<bool> UpdateAsync(Student obj)
        {
            _context.Students.Update(obj);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task DeleteAsync(Student obj)
        {
            _context.Students.Remove(obj);
            await _context.SaveChangesAsync();
        }

        public async Task<SchoolClass?> GetClassByNameAsync(string className)
        {
            return await _context.Set<SchoolClass>().FirstOrDefaultAsync(c => c.Name == className);
        }

    }
}
