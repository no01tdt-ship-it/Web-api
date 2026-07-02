using Web_api.Models;
using Web_api.ViewModels.Common;
using Web_api.ViewModels.Student;

namespace Web_api.Interfaces.IRepositories
{
    public interface IStudentRepository
    {
        // Hàm thêm mới một học sinh vào bộ nhớ đệm
        Task AddAsync(Student student);
        // Hàm chính thức nhấn nút "Lưu" để đẩy dữ liệu xuống SQL Server
        Task<bool> SaveChangesAsync();

        Task<List<Student>> GetAllAsync();
        Task<DTResult<ListStudentViewModel>> ListServerSide(StudentViewModelParameters parameters);
        Task<Student?> GetByIdAsync(long id);

        Task<bool> UpdateAsync(Student obj);
        Task DeleteAsync(Student obj);
        Task<SchoolClass?> GetClassByIdAsync(long? Id);
        Task<Province?> GetProvinceByIdAsync(long? Id);
        Task<Ward?> GetWardByIdAsync(long? Id);



    }
}
