using Web_api.Models;
using Web_api.ViewModels.Student;

namespace Web_api.Interfaces.IServices
{
    public interface IStudentService
    {
        Task<bool> CreateStudentAsync(AddStudentViewModel viewModel);
    }
}
