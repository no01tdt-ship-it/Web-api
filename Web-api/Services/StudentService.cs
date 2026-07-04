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
            var schoolClass = await _studentRepository.GetClassByIdAsync(viewModel.SchoolClassId);
            if (schoolClass == null)
            {
                return false;
            }
            var province = await _studentRepository.GetProvinceByIdAsync(viewModel.ProvinceId);
            if (province == null)
            {
                return false;
            }
            var ward = await _studentRepository.GetWardByIdAsync(viewModel.WardId);
            if (ward == null)
            {
                return false;
            }
            if (ward.ProvinceId != province.Id)
            {
                return false;
            }
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
                ProvinceId = province?.Id,
                WardId = ward?.Id,
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
                    ClassName = s.SchoolClass?.Name ?? "Chưa xếp lớp",
                    PhoneNumber = s.PhoneNumber,
                    GenderText = s.Gender == GenderConstant.Nam ? "Nam" :
                                 s.Gender == GenderConstant.Nu ? "Nữ" :
                                 s.Gender == GenderConstant.Khac ? "Khác" : "Chưa chọn",
                    Province = s.Province != null ? s.Province.Name : "",
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
                ClassName = s.SchoolClass?.Name,
                Image = s.Image,
                Gender = s.Gender,
                GenderText = s.Gender == GenderConstant.Nam ? "Nam" :
                             s.Gender == GenderConstant.Nu ? "Nữ" :
                             s.Gender == GenderConstant.Khac ? "Khác" : "Chưa chọn",
                Email = s.Email,
                BirthDay = s.BirthDay,
                CitizenId = s.CitizenId,
                PhoneNumber = s.PhoneNumber,
                Province = s.Province != null ? s.Province.Name : "",
                Ward = s.Ward != null ? s.Ward.Name : "",
                Address = s.Address,
                Course = s.Course,
                IsRetained = s.IsRetained,
                IsRetainedText = s.IsRetained ? "Bảo lưu" : "Đang học",
                ProvinceId = s.ProvinceId,
                WardId = s.WardId,
                SchoolClassId = s.SchoolClassId
            };

        }

        public async Task<bool> UpdateAsync(UpdateStudentViewModel model)
        {
            var student = await _studentRepository.GetByIdAsync(model.Id);
            if (student == null) return false;
            var schoolClass = await _studentRepository.GetClassByIdAsync(model.SchoolClassId);
            if (schoolClass == null) return false;
            var province = await _studentRepository.GetProvinceByIdAsync(model.ProvinceId);
            if (province == null) return false;
            var ward = await _studentRepository.GetWardByIdAsync(model.WardId);
            if (ward == null) return false;
            if (ward.ProvinceId != province.Id) return false;

            student.Name = model.Name;
            student.SchoolClassId = schoolClass.Id;
            student.Gender = model.Gender;
            student.Email = model.Email;
            student.BirthDay = model.BirthDay;
            student.CitizenId = model.CitizenId;
            student.PhoneNumber = model.PhoneNumber;
            student.ProvinceId = province?.Id;
            student.WardId = ward?.Id;
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

        public async Task<List<SchoolClass>> GetClassesAsync()
        {
            return await _studentRepository.GetClassesAsync();
        }

        public async Task<List<Province>> GetProvincesAsync()
        {
            return await _studentRepository.GetProvincesAsync();
        }

        public async Task<List<Ward>> GetWardsByProvinceIdAsync(long provinceId)
        {
            return await _studentRepository.GetWardsByProvinceIdAsync(provinceId);
        }
    }
}
