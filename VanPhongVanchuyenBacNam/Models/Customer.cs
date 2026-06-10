using System.ComponentModel.DataAnnotations;

namespace VanPhongVanchuyenBacNam.Models;

// Frequent sender saved for quick re-use when creating shipments.
public class Customer
{
    public int Id { get; set; }

    [Display(Name = "Tên khách hàng")]
    [Required(ErrorMessage = "Vui lòng nhập tên khách hàng.")]
    [StringLength(100, ErrorMessage = "Tên khách hàng tối đa {1} ký tự.")]
    public string Name { get; set; } = string.Empty;

    [Display(Name = "Số điện thoại")]
    [Required(ErrorMessage = "Vui lòng nhập số điện thoại.")]
    [StringLength(20, ErrorMessage = "Số điện thoại tối đa {1} ký tự.")]
    [RegularExpression(@"^[0-9+][0-9 .\-]{7,18}$", ErrorMessage = "Số điện thoại không hợp lệ.")]
    public string Phone { get; set; } = string.Empty;

    [Display(Name = "Địa chỉ")]
    [StringLength(250, ErrorMessage = "Địa chỉ tối đa {1} ký tự.")]
    public string? Address { get; set; }

    [Display(Name = "Ghi chú")]
    [StringLength(500, ErrorMessage = "Ghi chú tối đa {1} ký tự.")]
    public string? Note { get; set; }

    [Display(Name = "Ngày tạo")]
    public DateTime CreatedDate { get; set; } = DateTime.Now;
}
