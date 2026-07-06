using Web_api.ViewModels.Parameters;

namespace Web_api.ViewModels.Province
{
    public class ProvinceViewModelParameters : BaseParameters
    {
        // Ô tìm kiếm nhanh riêng của màn hình tỉnh/thành phố (nếu có)
        public string? SearchTExt { get; set; }
        // Bộ tham số bắt buộc phải có để JQuery DataTables vận hành Server-side
        public int Draw { get; set; }
        // Bộ tham số phục vụ sắp xếp động (Sorting)
        public string? SortColumn { get; set; }
        public string? SortDirection { get; set; }

    }
}
