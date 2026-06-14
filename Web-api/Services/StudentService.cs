using Web_api.Interfaces.IRepositories;
using Web_api.Interfaces.IServices;
using Web_api.Models;
using Web_api.ViewModels.Student;

namespace Web_api.Services
{
    public class StudentService : IStudentService
    {

        private readonly IStudentRepository _studentRepository;
        public StudentService(IStudentRepository studentRepository)
        {
            _studentRepository = studentRepository;
        }

       public async Task<bool> CreateStudentAsync(AddStudentViewModel viewModel)
        {
            var student = new Student
            {
                Name = viewModel.Name,
                ClassName = viewModel.ClassName,
                Image = viewModel.Image,
                Gender = viewModel.Gender,
                Email = viewModel.Email,
                BirthDay = viewModel.BirthDay,
                CitizenId = viewModel.CitizenId,
                PhoneNumber = viewModel.PhoneNumber,
                Province = viewModel.Province,
                Ward = viewModel.Ward,
                Address = viewModel.Address,
                Course = viewModel.Course
            };
            await _studentRepository.AddAsync(student);
            return await _studentRepository.SaveChangesAsync();
        }
        
    }
}
