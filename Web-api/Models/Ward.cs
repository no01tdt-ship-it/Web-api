namespace Web_api.Models
{
    public class Ward
    {
        public long Id { get; set; }
        public string Name { get; set; } = null!;
        public string? Code { get; set; }
        public long ProvinceId { get; set; }
        public bool Active { get; set; } = true;

        public virtual Province Province { get; set; } = null!;


    }
}
