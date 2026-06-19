namespace Web_api.Models
{
    public class Student
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string ClassName { get; set; }
        public string Image { get; set; }
        public int? Gender { get; set; }
        public string Email { get; set; }
        public DateTime BirthDay { get; set; }
        public DateTime CreatedDate { get; set; } = DateTime.Now;
        public string CitizenId { get; set; }
        public string PhoneNumber { get; set; }
        public string Province { get; set; }
        public string Ward { get; set; }
        public string Address { get; set; }
        public string Course { get; set; }
        public bool IsRetained { get; set; }
    }
}
