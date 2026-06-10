using System.ComponentModel.DataAnnotations;

namespace VanPhongVanchuyenBacNam.Models.ViewModels;

public class ChangePasswordViewModel
{
    [Display(Name = "Mật khẩu hiện tại")]
    [Required(ErrorMessage = "Vui lòng nhập mật khẩu hiện tại.")]
    [DataType(DataType.Password)]
    public string CurrentPassword { get; set; } = string.Empty;

    [Display(Name = "Mật khẩu mới")]
    [Required(ErrorMessage = "Vui lòng nhập mật khẩu mới.")]
    [StringLength(100, MinimumLength = 6, ErrorMessage = "Mật khẩu mới tối thiểu {2} ký tự.")]
    [DataType(DataType.Password)]
    public string NewPassword { get; set; } = string.Empty;

    [Display(Name = "Nhập lại mật khẩu mới")]
    [DataType(DataType.Password)]
    [Compare(nameof(NewPassword), ErrorMessage = "Mật khẩu nhập lại không khớp.")]
    public string ConfirmPassword { get; set; } = string.Empty;
}
