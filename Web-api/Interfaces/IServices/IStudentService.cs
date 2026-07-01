using Web_api.Models;
using Web_api.ViewModels.Common;
using Web_api.ViewModels.Student;

namespace Web_api.Interfaces.IServices
{
    public interface IStudentService
    {
        Task<bool> CreateStudentAsync(AddStudentViewModel viewModel);

        Task<DTResult<ListStudentViewModel>> ListServerSide(StudentViewModelParameters parameters);

        Task<List<ListStudentViewModel>> GetAllAsync();

        Task<DetailStudentViewModel> GetByIdAsync(long id);

        Task<bool> UpdateAsync(UpdateStudentViewModel model);

        Task DeleteAsync(long id);
    }
}
