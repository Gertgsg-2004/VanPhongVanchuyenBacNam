using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using VanPhongVanchuyenBacNam.Data;
using VanPhongVanchuyenBacNam.Models;
using VanPhongVanchuyenBacNam.Models.ViewModels;

namespace VanPhongVanchuyenBacNam.Controllers;

public class ReportsController : Controller
{
    private readonly AppDbContext _context;

    public ReportsController(AppDbContext context)
    {
        _context = context;
    }

    // GET: Reports/CodReconciliation?fromDate=...&toDate=...
    // Per-company totals used to reconcile COD and fees with each transport company.
    public async Task<IActionResult> CodReconciliation(DateTime? fromDate, DateTime? toDate)
    {
        var from = (fromDate ?? DateTime.Today).Date;
        var to = (toDate ?? DateTime.Today).Date;
        if (to < from)
        {
            (from, to) = (to, from);
        }
        var end = to.AddDays(1);

        var rows = await _context.Shipments
            .Where(s => s.CreatedDate >= from && s.CreatedDate < end
                        && s.Status != ShipmentStatus.Cancelled)
            .GroupBy(s => s.TransportCompany == null ? "(Chưa chọn nhà xe)" : s.TransportCompany.Name)
            .Select(g => new CodReportRow
            {
                CompanyName = g.Key,
                ShipmentCount = g.Count(),
                DeliveredCount = g.Count(s => s.Status == ShipmentStatus.Delivered),
                TotalFee = g.Sum(s => s.ShippingFee),
                TotalCod = g.Sum(s => s.CODAmount)
            })
            .OrderBy(r => r.CompanyName)
            .ToListAsync();

        var model = new CodReportViewModel
        {
            FromDate = from,
            ToDate = to,
            Rows = rows
        };

        return View(model);
    }
}
