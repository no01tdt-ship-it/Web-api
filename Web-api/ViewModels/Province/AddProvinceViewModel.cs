using System.ComponentModel.DataAnnotations;

namespace Web_api.ViewModels.Province
{
    public class AddProvinceViewModel
    {
        [Required(ErrorMessage = "Tên tỉnh/thành phố không được để trống.")]
        [MaxLength(100, ErrorMessage = "Tên tỉnh/thành phố không được vượt quá 100 ký tự.")]
        public string Name { get; set; } = null!;
        [Required(ErrorMessage = "Mã định danh tỉnh/thành phố không được để trống.")]
        [MaxLength(50, ErrorMessage = "Mã định danh tỉnh/thành phố không được vượt quá 50 ký tự.")]
        public string Code { get; set; } = null!;
    }
}
