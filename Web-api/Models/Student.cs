namespace Web_api.Models
{
    public class Student
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public long? SchoolClassId { get; set; }
        public string Image { get; set; }
        public int? Gender { get; set; }
        public string Email { get; set; }
        public DateTime BirthDay { get; set; }
        public DateTime CreatedDate { get; set; } = DateTime.Now;
        public string CitizenId { get; set; }
        public string PhoneNumber { get; set; }

        public string Address { get; set; }
        public string Course { get; set; }
        public bool IsRetained { get; set; }
        public bool Active { get; set; } = true;

        public long? ProvinceId { get; set; }
        public long? WardId { get; set; }

        public virtual Province? Province { get; set; }
        public virtual Ward? Ward { get; set; }
        public SchoolClass? SchoolClass { get; set; }
    }
}
