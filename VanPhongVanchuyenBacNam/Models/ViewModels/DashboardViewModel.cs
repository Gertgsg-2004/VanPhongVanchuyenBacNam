namespace VanPhongVanchuyenBacNam.Models.ViewModels;

public class DashboardViewModel
{
    public int TodayCount { get; set; }

    public decimal TodayShippingFee { get; set; }

    public decimal TodayCod { get; set; }

    public int DeliveredTodayCount { get; set; }

    public int UnpaidCount { get; set; }

    public Dictionary<ShipmentStatus, int> StatusCounts { get; set; } = new();

    public List<Shipment> RecentShipments { get; set; } = new();
}
