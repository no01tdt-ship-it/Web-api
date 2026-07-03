namespace Web_api.Models
{
    public class SchoolLevel
    {
        public long Id { get; set; }
        public string Name { get; set; } = null!;
        public string? Code { get; set; }
        public bool Active { get; set; }
        public DateTime CreatedDate { get; set; }

        public ICollection<School> Schools { get; set; } = new List<School>();
    }
}
