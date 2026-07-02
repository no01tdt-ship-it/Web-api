namespace Web_api.Models
{
    public class School
    {
        public long Id { get; set; }
        public string Name { get; set; } = null!;
        public long SchoolLevelId { get; set; }
        public SchoolLevel SchoolLevel { get; set; } = null!;
        public string? Code { get; set; }
        public long? ProvinceId { get; set; }
        public long? WardId { get; set; }
        public virtual Province? Province { get; set; }
        public virtual Ward? Ward { get; set; }
        public string? Address { get; set; }
        public bool Active { get; set; } = true;
        public ICollection<SchoolClass> SchoolClasses { get; set; } = new List<SchoolClass>();
    }
}
