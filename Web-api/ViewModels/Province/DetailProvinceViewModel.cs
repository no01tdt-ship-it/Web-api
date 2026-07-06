namespace Web_api.ViewModels.Province
{
    public class DetailProvinceViewModel
    {
        public long Id { get; set; }
        public string Name { get; set; } = null!;
        public string Code { get; set; } = null!;
        public bool Active { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
