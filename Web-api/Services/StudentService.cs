using Web_api.Interfaces.IRepositories;
using Web_api.Interfaces.IServices;
using Web_api.Models;
using Web_api.ViewModels.Common;
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

        public async Task<DTResult<ListStudentViewModel>> ListServerSide(StudentViewModelParameters parameters)
        {
            return await _studentRepository.ListServerSide(parameters);


        }

        public async Task<List<ListStudentViewModel>> GetAllAsync()
        {
            var students = await _studentRepository.GetAllAsync();
            var result = new List<ListStudentViewModel>();
            foreach (var s in students)
            {
                result.Add(new ListStudentViewModel
                {
                    Id = s.Id,
                    Name = s.Name,
                    ClassName = s.ClassName,
                    PhoneNumber = s.PhoneNumber,
                    GenderText = s.Gender ? "Nam" : "Nữ",
                    Province = s.Province,
                    Email = s.Email,
                    IsRetained = s.IsRetained
                });
            }

            return result;
        }

        public async Task<DetailStudentViewModel> GetByIdAsync(int id){
            var s =  await _studentRepository.GetByIdAsync(id);
            if(s == null) return null;
            return new DetailStudentViewModel{
                Name = s.Name,
                ClassName = s.ClassName,
                Image = s.Image,
                Gender = s.Gender,
                Gender = s.Gender ? "Nam" : "Nữ",
                Email = s.Email,
                BirthDay = s.BirthDay,
                CitizenId = s.CitizenId,
                PhoneNumber = s.PhoneNumber,
                Province = s.Province,
                Ward = s.Ward,
                Address = s.Address,
                Course = s.Course,
                IsRetained = s.IsRetained? "Bảo lưu" : "Đang học"
            }

        }

    }
}
