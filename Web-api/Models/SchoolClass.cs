namespace Web_api.Models
{
    public class SchoolClass
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public int? Grade { get; set; }
        public long SchoolId { get; set; }
        public School School { get; set; }
        public bool Active { get; set; } = true;
        public ICollection<Student> Students { get; set; } = new List<Student>();


    }
}
