using System.ComponentModel.DataAnnotations;

namespace Web_api.ViewModels.Student
{
    public class AddStudentViewModel
    {
       [Required(ErrorMessage = "Họ và tên không được để trống.")]
        [MaxLength(100, ErrorMessage = "Họ và tên không được vượt quá 100 ký tự.")]
       public string Name { get; set; }
       [Required(ErrorMessage = "Tên lớp không được để trống.")]
       [MaxLength(50,ErrorMessage = "Tên lớp không được vượt quá 50 ký tự.")]
       public string ClassName {get; set;}
       [MaxLength(250, ErrorMessage = "Ảnh không được vượt quá 250 ký tự.")]
       public string? Image {get;set;}
       [Required(ErrorMessage = "Giới tính không được để trống.")]
       public bool Gender {get;set;}
       [Required(ErrorMessage = "Email không được để trống.")]
       [MaxLength(50, ErrorMessage = "Email không được vượt quá 50 ký tự.")]
       [EmailAddress(ErrorMessage = "Email không đúng định dạng.")]
       public string Email {get;set;}
       [Required(ErrorMessage = "Ngày sinh không được để trống.")]
       public DateTime BirthDay { get; set; }
       [Required(ErrorMessage = "CMND không được để trống.")]
        [MaxLength(12, ErrorMessage = "CMND không được vượt quá 12 ký tự.")]
       public string CitizenId { get; set; }
       [Required(ErrorMessage = "Số điện thoại không được để trống.")]
       [MaxLength(12, ErrorMessage = "Số điện thoại không được vượt quá 12 ký tự.")]
        [RegularExpression(@"^(0|\+84)[0-9]{9}$",ErrorMessage = "Số điện thoại không hợp lệ.")]
        [Phone(ErrorMessage = "Số điện thoại không đúng định dạng.")]
       public string PhoneNumber { get; set; }
       [Required(ErrorMessage = "Tỉnh không được để trống.")]
       [StringLength(50, ErrorMessage = "Tỉnh không được vượt quá 50 ký tự.")]
       public string Province { get; set; }
       [StringLength(50, ErrorMessage = "Phường không được vượt quá 50 ký tự.")]
       public string? Ward { get; set; }
       [StringLength(250, ErrorMessage = "Địa chỉ không được vượt quá 250 ký tự.")]
       public string? Address { get; set; }
       [StringLength(50, ErrorMessage = "Khóa học không được vượt quá 50 ký tự.")]
       public string? Course { get; set; }
       [Required(ErrorMessage = "Trạng thái không được để trống.")]
       public bool? IsRetained { get; set; }
    }
}
