using System.ComponentModel.DataAnnotations;

namespace VanPhongVanchuyenBacNam.Models;

public enum ShipmentStatus
{
    [Display(Name = "Đã nhận")]
    Received = 0,

    [Display(Name = "Đang vận chuyển")]
    InTransit = 1,

    [Display(Name = "Đã giao")]
    Delivered = 2,

    [Display(Name = "Đã hủy")]
    Cancelled = 3
}
