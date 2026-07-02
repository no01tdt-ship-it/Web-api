namespace Web_api.Models
{
    public class Province
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public bool Active { get; set; } = true;

        public virtual ICollection<Ward> Wards { get; set; } = new List<Ward>();
    }
}
