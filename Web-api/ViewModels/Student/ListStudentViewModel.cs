namespace Web_api.ViewModels.Student
{
    public class ListStudentViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string ClassName { get; set; } = null!;
        public string CitizenId { get; set; } = null!;
        public string PhoneNumber { get; set; } = null!;
        public string GenderText { get; set; } = null!;
        public string Province { get; set; } = null!;
        public string Email { get; set; } = null!;
        public bool IsRetained { get; set; }
    }
}
