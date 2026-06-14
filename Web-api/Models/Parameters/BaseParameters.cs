namespace Web_api.Models.Parameters
{
    public class BaseParameters
    {
        // Số trang muốn xem (Mặc định là trang 1 nếu client không truyền)
        public int Start { get; set; }
        // Số lượng dòng dữ liệu trên 1 trang (Mặc định là 10 dòng)
        public int Length { get; set; }
        // Ô tìm kiếm nhanh chung toàn màn hình (Mặc định để chuỗi rỗng)
        public string? SearchAll { get; set; }
    }
}
