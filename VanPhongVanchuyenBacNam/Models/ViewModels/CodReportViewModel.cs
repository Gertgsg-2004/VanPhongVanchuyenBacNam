namespace VanPhongVanchuyenBacNam.Models.ViewModels;

// COD reconciliation with transport companies over a date range.
public class CodReportViewModel
{
    public DateTime FromDate { get; set; }

    public DateTime ToDate { get; set; }

    public List<CodReportRow> Rows { get; set; } = new();
}

public class CodReportRow
{
    public string CompanyName { get; set; } = string.Empty;

    public int ShipmentCount { get; set; }

    public int DeliveredCount { get; set; }

    public decimal TotalFee { get; set; }

    public decimal TotalCod { get; set; }
}
