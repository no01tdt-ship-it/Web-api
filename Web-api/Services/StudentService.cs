using Web_api.Constants;
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
            var schoolClass = await _studentRepository.GetClassByNameAsync(viewModel.ClassName);
            var student = new Student
            {
                Name = viewModel.Name,
                SchoolClassId = schoolClass?.Id,
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
                    ClassName =  s.SchoolClass?.Name ?? "Chưa xếp lớp",
                    PhoneNumber = s.PhoneNumber,
                    GenderText = s.Gender == GenderConstant.Nam ? "Nam" :
                                 s.Gender == GenderConstant.Nu ? "Nữ" :
                                 s.Gender == GenderConstant.Khac ? "Khác" : "Chưa chọn",
                    Province = s.Province,
                    Email = s.Email,
                    IsRetained = s.IsRetained
                });
            }

            return result;
        }

        public async Task<DetailStudentViewModel> GetByIdAsync(long id)
        {
            var s = await _studentRepository.GetByIdAsync(id);
            if (s == null) return null;
            return new DetailStudentViewModel
            {
                Name = s.Name,
                ClassName = s.SchoolClass?.Name ?? "Chưa xếp lớp",
                Image = s.Image,
                Gender = s.Gender,
                GenderText = s.Gender == GenderConstant.Nam ? "Nam" :
                             s.Gender == GenderConstant.Nu ? "Nữ" :
                             s.Gender == GenderConstant.Khac ? "Khác" : "Chưa chọn",
                Email = s.Email,
                BirthDay = s.BirthDay,
                CitizenId = s.CitizenId,
                PhoneNumber = s.PhoneNumber,
                Province = s.Province,
                Ward = s.Ward,
                Address = s.Address,
                Course = s.Course,
                IsRetained = s.IsRetained,
                IsRetainedText = s.IsRetained ? "Bảo lưu" : "Đang học"
            };

        }

        public async Task<bool> UpdateAsync(UpdateStudentViewModel model)
        {
            var student = await _studentRepository.GetByIdAsync(model.Id);
            if (student == null) return false;
            var schoolClass = await _studentRepository.GetClassByNameAsync(model.ClassName);

            student.Name = model.Name;
            student.SchoolClassId = schoolClass?.Id;
            student.Gender = model.Gender;
            student.Email = model.Email;
            student.BirthDay = model.BirthDay;
            student.CitizenId = model.CitizenId;
            student.PhoneNumber = model.PhoneNumber;
            student.Province = model.Province;
            student.Ward = model.Ward;
            student.Address = model.Address;
            student.Course = model.Course;
            student.IsRetained = model.IsRetained;

            if (!string.IsNullOrWhiteSpace(model.Image))
            {
                student.Image = model.Image;
            }

            return await _studentRepository.UpdateAsync(student);
        }

        public async Task DeleteAsync(long id)
        {
            var student = await _studentRepository.GetByIdAsync(id);
            if (student == null)
            {
                throw new Exception($"Không tìm thấy học sinh mang mã số {id} để xóa!");
            }

            await _studentRepository.DeleteAsync(student);
        }
    }
}
