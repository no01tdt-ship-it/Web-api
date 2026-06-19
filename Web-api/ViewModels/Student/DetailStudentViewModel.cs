using System.ComponentModel.DataAnnotations;

namespace Web_api.ViewModels.Student
{
    public class DetailStudentViewModel
    {

        public string Name { get; set; }

        public string ClassName { get; set; }
        public string? Image { get; set; }
        public bool Gender { get; set; }
        public string GenderText { get; set; } = null;
        public string Email { get; set; }
        public DateTime BirthDay { get; set; }
        public string CitizenId { get; set; }
        public string PhoneNumber { get; set; }
        public string Province { get; set; }
        public string? Ward { get; set; }
        public string? Address { get; set; }
        public string? Course { get; set; }
        public bool? IsRetained { get; set; }
        public string IsRetainedText { get; set; } = null;
    }
}
