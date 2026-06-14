using Web_api.Models;

namespace Web_api.Interfaces.IRepositories
{
    public interface IStudentRepository
    {
        // Hàm thêm mới một học sinh vào bộ nhớ đệm
        Task AddAsync(Student student);
        // Hàm chính thức nhấn nút "Lưu" để đẩy dữ liệu xuống SQL Server
        Task<bool> SaveChangesAsync();
    }
}
