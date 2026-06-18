using Web_api.ViewModels.Parameters;

namespace Web_api.ViewModels.Student
{
    public class StudentViewModelParameters : BaseParameters
    {
        // 1. Ô tìm kiếm nhanh chung toàn màn hình
        public string? SearchText { get; set; }

        // 2. Bộ tham số bắt buộc phải có để JQuery DataTables vận hành Server-side
        public int Draw { get; set; }   // Số thứ tự request để đồng bộ phía giao diện
        public int Start { get; set; }  // Dòng dòng bắt đầu lấy (Ví dụ: dòng 0, dòng 10, dòng 20...)
        public int Length { get; set; } // Số lượng dòng muốn lấy trên 1 trang (Ví dụ: 10, 25, 50 dòng)

        // 3. Bộ tham số phục vụ việc Click vào đầu cột để Sắp xếp (Sorting) giống công ty
        public string? SortColumn { get; set; }    // Tên cột muốn sắp xếp (Ví dụ: "Name", "ClassName")
        public string? SortDirection { get; set; } // Chiều sắp xếp: "asc" (tăng dần) hoặc "desc" (giảm dần)

        // 4. Các bộ lọc riêng (Filters) đặc thù của riêng màn hình Học sinh (nếu sau này cần)
        // public string? ClassName { get; set; }
        // public bool? IsRetained { get; set; }

    }
}
