using System.ComponentModel.DataAnnotations;

namespace VanPhongVanchuyenBacNam.Models;

public class TransportCompany
{
    public int Id { get; set; }

    [Display(Name = "Tên nhà xe")]
    [Required(ErrorMessage = "Vui lòng nhập tên nhà xe.")]
    [StringLength(150, ErrorMessage = "Tên nhà xe tối đa {1} ký tự.")]
    public string Name { get; set; } = string.Empty;

    [Display(Name = "Tỉnh/thành phục vụ")]
    [Required(ErrorMessage = "Vui lòng nhập tỉnh/thành phục vụ.")]
    [StringLength(100, ErrorMessage = "Tỉnh/thành tối đa {1} ký tự.")]
    public string Province { get; set; } = string.Empty;

    [Display(Name = "SĐT chính")]
    [Required(ErrorMessage = "Vui lòng nhập số điện thoại chính.")]
    [StringLength(20, ErrorMessage = "Số điện thoại tối đa {1} ký tự.")]
    [RegularExpression(@"^[0-9+][0-9 .\-]{7,18}$", ErrorMessage = "Số điện thoại không hợp lệ.")]
    public string PhoneNumber { get; set; } = string.Empty;

    [Display(Name = "SĐT phụ")]
    [StringLength(20, ErrorMessage = "Số điện thoại tối đa {1} ký tự.")]
    [RegularExpression(@"^[0-9+][0-9 .\-]{7,18}$", ErrorMessage = "Số điện thoại không hợp lệ.")]
    public string? BackupPhone { get; set; }

    [Display(Name = "Địa chỉ bến/văn phòng")]
    [StringLength(250, ErrorMessage = "Địa chỉ tối đa {1} ký tự.")]
    public string? Address { get; set; }

    [Display(Name = "Người liên hệ")]
    [StringLength(100, ErrorMessage = "Người liên hệ tối đa {1} ký tự.")]
    public string? ContactPerson { get; set; }

    [Display(Name = "Ghi chú")]
    [StringLength(500, ErrorMessage = "Ghi chú tối đa {1} ký tự.")]
    public string? Note { get; set; }

    [Display(Name = "Đang hợp tác")]
    public bool IsActive { get; set; } = true;
}
