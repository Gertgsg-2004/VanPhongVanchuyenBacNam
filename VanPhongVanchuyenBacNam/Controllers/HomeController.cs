using System.Diagnostics;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using VanPhongVanchuyenBacNam.Data;
using VanPhongVanchuyenBacNam.Models;
using VanPhongVanchuyenBacNam.Models.ViewModels;

namespace VanPhongVanchuyenBacNam.Controllers;

public class HomeController : Controller
{
    private readonly AppDbContext _context;

    public HomeController(AppDbContext context)
    {
        _context = context;
    }

    // Dashboard: today's totals, pipeline by status, latest shipments.
    public async Task<IActionResult> Index()
    {
        var today = DateTime.Today;
        var tomorrow = today.AddDays(1);

        var todayShipments = _context.Shipments
            .Where(s => s.CreatedDate >= today && s.CreatedDate < tomorrow);

        // Cancelled shipments don't count towards revenue. Amounts are summed in memory
        // because not every database provider supports SUM over decimal columns.
        var todayAmounts = await todayShipments
            .Where(s => s.Status != ShipmentStatus.Cancelled)
            .Select(s => new { s.ShippingFee, s.CODAmount })
            .ToListAsync();

        var model = new DashboardViewModel
        {
            TodayCount = await todayShipments.CountAsync(),
            TodayShippingFee = todayAmounts.Sum(s => s.ShippingFee),
            TodayCod = todayAmounts.Sum(s => s.CODAmount),
            DeliveredTodayCount = await _context.Shipments
                .CountAsync(s => s.DeliveredDate >= today && s.DeliveredDate < tomorrow),
            UnpaidCount = await _context.Shipments
                .CountAsync(s => !s.IsPaid && s.Status != ShipmentStatus.Cancelled),
            StatusCounts = await _context.Shipments
                .GroupBy(s => s.Status)
                .Select(g => new { g.Key, Count = g.Count() })
                .ToDictionaryAsync(x => x.Key, x => x.Count),
            RecentShipments = await _context.Shipments
                .Include(s => s.TransportCompany)
                .OrderByDescending(s => s.CreatedDate)
                .Take(5)
                .ToListAsync()
        };

        return View(model);
    }

    [AllowAnonymous]
    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
