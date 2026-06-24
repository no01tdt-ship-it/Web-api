namespace Web_api.Models
{
    public class School
    {
        public long Id { get; set; }
        public string Name { get; set; } = null!;
        public string? Code { get; set; }
        public string? Province { get; set; }
        public string? Ward { get; set; }
        public string? Address { get; set; }
        public bool Active { get; set; } = true;
        public ICollection<SchoolClass> SchoolClasses { get; set; } = new List<SchoolClass>();
    }
}
