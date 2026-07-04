using System.ComponentModel.DataAnnotations;

namespace Web_api.ViewModels.AdminMenu
{
    public class UpdateAdminMenuViewModel
    {
        /// <summary>
        /// Mã định danh của menu.
        /// </summary>
        [Required(ErrorMessage = "ID không được để trống.")]
        public long Id { get; set; }

        /// <summary>
        /// Mã menu cha (có thể null nếu là menu gốc).
        /// </summary>
        public long? ParentId { get; set; }

        /// <summary>
        /// Mã chức năng menu (Dùng để định danh, duy nhất).
        /// </summary>
        [Required(ErrorMessage = "Mã menu không được để trống.")]
        [MaxLength(100, ErrorMessage = "Mã menu không được vượt quá 100 ký tự.")]
        [RegularExpression(@"^[a-zA-Z0-9_\-]+$", ErrorMessage = "Mã menu chỉ được phép chứa các ký tự chữ, số, gạch dưới (_) và gạch ngang (-).")]
        public string MenuCode { get; set; } = null!;

        /// <summary>
        /// Tên menu hiển thị.
        /// </summary>
        [Required(ErrorMessage = "Tên menu không được để trống.")]
        [MaxLength(250, ErrorMessage = "Tên menu không được vượt quá 250 ký tự.")]
        public string MenuName { get; set; } = null!;

        /// <summary>
        /// Đường dẫn menu (Url).
        /// </summary>
        [MaxLength(250, ErrorMessage = "Đường dẫn không được vượt quá 250 ký tự.")]
        public string? Url { get; set; }

        /// <summary>
        /// Biểu tượng menu (CSS class icon).
        /// </summary>
        [MaxLength(250, ErrorMessage = "Icon không được vượt quá 250 ký tự.")]
        public string? Icon { get; set; }

        /// <summary>
        /// Thứ tự hiển thị.
        /// </summary>
        [Required(ErrorMessage = "Thứ tự hiển thị không được để trống.")]
        public int SortOrder { get; set; }

        /// <summary>
        /// Trạng thái hoạt động.
        /// </summary>
        public bool Active { get; set; } = true;

        /// <summary>
        /// Menu hệ thống.
        /// </summary>
        public bool IsSystem { get; set; } = false;

        /// <summary>
        /// Mô tả chi tiết.
        /// </summary>
        [MaxLength(500, ErrorMessage = "Mô tả không được vượt quá 500 ký tự.")]
        public string? Description { get; set; }
    }
}
