using System.ComponentModel.DataAnnotations;

namespace VanPhongVanchuyenBacNam.Models;

public class Shipment
{
    public int Id { get; set; }

    // Generated server-side (VP + yyyyMMdd + daily sequence), never entered by hand.
    [Display(Name = "Mã vận đơn")]
    [StringLength(20)]
    public string TrackingCode { get; set; } = string.Empty;

    [Display(Name = "Tên người gửi")]
    [Required(ErrorMessage = "Vui lòng nhập tên người gửi.")]
    [StringLength(100, ErrorMessage = "Tên người gửi tối đa {1} ký tự.")]
    public string SenderName { get; set; } = string.Empty;

    [Display(Name = "SĐT người gửi")]
    [Required(ErrorMessage = "Vui lòng nhập SĐT người gửi.")]
    [StringLength(20, ErrorMessage = "Số điện thoại tối đa {1} ký tự.")]
    [RegularExpression(@"^[0-9+][0-9 .\-]{7,18}$", ErrorMessage = "Số điện thoại không hợp lệ.")]
    public string SenderPhone { get; set; } = string.Empty;

    [Display(Name = "Địa chỉ người gửi")]
    [StringLength(250, ErrorMessage = "Địa chỉ tối đa {1} ký tự.")]
    public string? SenderAddress { get; set; }

    [Display(Name = "Tên người nhận")]
    [Required(ErrorMessage = "Vui lòng nhập tên người nhận.")]
    [StringLength(100, ErrorMessage = "Tên người nhận tối đa {1} ký tự.")]
    public string RecipientName { get; set; } = string.Empty;

    [Display(Name = "SĐT người nhận")]
    [Required(ErrorMessage = "Vui lòng nhập SĐT người nhận.")]
    [StringLength(20, ErrorMessage = "Số điện thoại tối đa {1} ký tự.")]
    [RegularExpression(@"^[0-9+][0-9 .\-]{7,18}$", ErrorMessage = "Số điện thoại không hợp lệ.")]
    public string RecipientPhone { get; set; } = string.Empty;

    [Display(Name = "Địa chỉ nhận")]
    [Required(ErrorMessage = "Vui lòng nhập địa chỉ nhận.")]
    [StringLength(250, ErrorMessage = "Địa chỉ tối đa {1} ký tự.")]
    public string RecipientAddress { get; set; } = string.Empty;

    [Display(Name = "Tỉnh nhận")]
    [Required(ErrorMessage = "Vui lòng nhập tỉnh/thành nhận.")]
    [StringLength(100, ErrorMessage = "Tỉnh/thành tối đa {1} ký tự.")]
    public string RecipientProvince { get; set; } = string.Empty;

    [Display(Name = "Mô tả hàng hóa")]
    [Required(ErrorMessage = "Vui lòng mô tả hàng hóa.")]
    [StringLength(250, ErrorMessage = "Mô tả tối đa {1} ký tự.")]
    public string GoodsDescription { get; set; } = string.Empty;

    [Display(Name = "Cân nặng (kg)")]
    [Range(0.01, 100000, ErrorMessage = "Cân nặng phải lớn hơn 0.")]
    public decimal? Weight { get; set; }

    [Display(Name = "Cước phí (đ)")]
    [Range(0, 1000000000, ErrorMessage = "Cước phí không hợp lệ.")]
    public decimal ShippingFee { get; set; }

    [Display(Name = "Tiền thu hộ COD (đ)")]
    [Range(0, 1000000000, ErrorMessage = "Tiền thu hộ không hợp lệ.")]
    public decimal CODAmount { get; set; }

    [Display(Name = "Đã thanh toán cước")]
    public bool IsPaid { get; set; }

    [Display(Name = "Trạng thái")]
    public ShipmentStatus Status { get; set; } = ShipmentStatus.Received;

    [Display(Name = "Nhà xe gửi đi")]
    public int? TransportCompanyId { get; set; }

    [Display(Name = "Nhà xe gửi đi")]
    public TransportCompany? TransportCompany { get; set; }

    [Display(Name = "Ngày gửi")]
    public DateTime CreatedDate { get; set; } = DateTime.Now;

    [Display(Name = "Ngày giao")]
    public DateTime? DeliveredDate { get; set; }

    [Display(Name = "Ghi chú")]
    [StringLength(500, ErrorMessage = "Ghi chú tối đa {1} ký tự.")]
    public string? Note { get; set; }
}
