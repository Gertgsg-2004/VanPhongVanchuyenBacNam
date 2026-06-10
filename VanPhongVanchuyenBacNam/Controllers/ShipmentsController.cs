using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using VanPhongVanchuyenBacNam.Data;
using VanPhongVanchuyenBacNam.Helpers;
using VanPhongVanchuyenBacNam.Models;

namespace VanPhongVanchuyenBacNam.Controllers;

public class ShipmentsController : Controller
{
    private readonly AppDbContext _context;

    public ShipmentsController(AppDbContext context)
    {
        _context = context;
    }

    // GET: Shipments?searchTerm=...&province=...&status=...
    public async Task<IActionResult> Index(string? searchTerm, string? province, ShipmentStatus? status)
    {
        var query = _context.Shipments
            .Include(s => s.TransportCompany)
            .AsQueryable();

        if (!string.IsNullOrWhiteSpace(searchTerm))
        {
            var term = searchTerm.Trim();
            query = query.Where(s =>
                s.TrackingCode.Contains(term) ||
                s.RecipientName.Contains(term) ||
                s.RecipientPhone.Contains(term) ||
                s.SenderName.Contains(term) ||
                s.SenderPhone.Contains(term));
        }

        if (!string.IsNullOrWhiteSpace(province))
        {
            query = query.Where(s => s.RecipientProvince == province);
        }

        if (status.HasValue)
        {
            query = query.Where(s => s.Status == status.Value);
        }

        ViewBag.Provinces = await _context.Shipments
            .Select(s => s.RecipientProvince)
            .Distinct()
            .OrderBy(p => p)
            .ToListAsync();
        ViewBag.SearchTerm = searchTerm;
        ViewBag.SelectedProvince = province;
        ViewBag.SelectedStatus = status;

        var shipments = await query
            .OrderByDescending(s => s.CreatedDate)
            .ToListAsync();

        return View(shipments);
    }

    // GET: Shipments/Details/5
    public async Task<IActionResult> Details(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var shipment = await _context.Shipments
            .Include(s => s.TransportCompany)
            .FirstOrDefaultAsync(s => s.Id == id);
        if (shipment == null)
        {
            return NotFound();
        }

        return View(shipment);
    }

    // GET: Shipments/Create — customerId pre-fills the sender from a saved customer.
    public async Task<IActionResult> Create(int? customerId)
    {
        await LoadFormDataAsync();

        var shipment = new Shipment();
        if (customerId.HasValue)
        {
            var customer = await _context.Customers.FindAsync(customerId.Value);
            if (customer != null)
            {
                shipment.SenderName = customer.Name;
                shipment.SenderPhone = customer.Phone;
                shipment.SenderAddress = customer.Address;
            }
        }

        return View(shipment);
    }

    // POST: Shipments/Create
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(Shipment shipment, bool saveAsCustomer = false)
    {
        if (!ModelState.IsValid)
        {
            await LoadFormDataAsync();
            return View(shipment);
        }

        shipment.TrackingCode = await GenerateTrackingCodeAsync();
        shipment.CreatedDate = DateTime.Now;
        ApplyStatusDates(shipment);

        _context.Add(shipment);
        await _context.SaveChangesAsync();

        TempData["SuccessMessage"] = $"Đã tạo vận đơn {shipment.TrackingCode}.";

        // Optionally remember the sender for next time (skip if the phone already exists).
        if (saveAsCustomer &&
            !await _context.Customers.AnyAsync(c => c.Phone == shipment.SenderPhone))
        {
            _context.Customers.Add(new Customer
            {
                Name = shipment.SenderName,
                Phone = shipment.SenderPhone,
                Address = shipment.SenderAddress
            });
            await _context.SaveChangesAsync();
            TempData["SuccessMessage"] += " Người gửi đã được lưu vào danh sách khách quen.";
        }

        return RedirectToAction(nameof(Details), new { id = shipment.Id });
    }

    // GET: Shipments/Edit/5
    public async Task<IActionResult> Edit(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var shipment = await _context.Shipments.FindAsync(id);
        if (shipment == null)
        {
            return NotFound();
        }

        await LoadFormDataAsync();
        return View(shipment);
    }

    // POST: Shipments/Edit/5
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, Shipment form)
    {
        if (id != form.Id)
        {
            return NotFound();
        }

        if (!ModelState.IsValid)
        {
            await LoadFormDataAsync();
            return View(form);
        }

        var shipment = await _context.Shipments.FindAsync(id);
        if (shipment == null)
        {
            return NotFound();
        }

        // Copy only the editable fields; TrackingCode/CreatedDate stay untouched
        // so a tampered form can never overwrite them.
        shipment.SenderName = form.SenderName;
        shipment.SenderPhone = form.SenderPhone;
        shipment.SenderAddress = form.SenderAddress;
        shipment.RecipientName = form.RecipientName;
        shipment.RecipientPhone = form.RecipientPhone;
        shipment.RecipientAddress = form.RecipientAddress;
        shipment.RecipientProvince = form.RecipientProvince;
        shipment.GoodsDescription = form.GoodsDescription;
        shipment.Weight = form.Weight;
        shipment.ShippingFee = form.ShippingFee;
        shipment.CODAmount = form.CODAmount;
        shipment.IsPaid = form.IsPaid;
        shipment.Status = form.Status;
        shipment.TransportCompanyId = form.TransportCompanyId;
        shipment.Note = form.Note;
        ApplyStatusDates(shipment);

        await _context.SaveChangesAsync();

        TempData["SuccessMessage"] = $"Đã cập nhật vận đơn {shipment.TrackingCode}.";
        return RedirectToAction(nameof(Details), new { id = shipment.Id });
    }

    // POST: Shipments/UpdateStatus/5 — quick status change from list/details pages.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> UpdateStatus(int id, ShipmentStatus status)
    {
        var shipment = await _context.Shipments.FindAsync(id);
        if (shipment == null)
        {
            return NotFound();
        }

        shipment.Status = status;
        ApplyStatusDates(shipment);
        await _context.SaveChangesAsync();

        TempData["SuccessMessage"] =
            $"Vận đơn {shipment.TrackingCode} chuyển sang trạng thái \"{status.GetDisplayName()}\".";
        return RedirectToAction(nameof(Details), new { id });
    }

    // GET: Shipments/PrintLabel/5
    public async Task<IActionResult> PrintLabel(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var shipment = await _context.Shipments
            .Include(s => s.TransportCompany)
            .FirstOrDefaultAsync(s => s.Id == id);
        if (shipment == null)
        {
            return NotFound();
        }

        // A single label is just a one-item print job — reuse the PrintLabels view.
        return View("PrintLabels", new List<Shipment> { shipment });
    }

    // GET: Shipments/PrintLabels?ids=1&ids=2 — print several labels at once.
    public async Task<IActionResult> PrintLabels(int[] ids)
    {
        if (ids == null || ids.Length == 0)
        {
            TempData["ErrorMessage"] = "Hãy chọn ít nhất một vận đơn để in nhãn.";
            return RedirectToAction(nameof(Index));
        }

        var shipments = await _context.Shipments
            .Include(s => s.TransportCompany)
            .Where(s => ids.Contains(s.Id))
            .OrderBy(s => s.Id)
            .ToListAsync();

        return View(shipments);
    }

    // Keeps DeliveredDate consistent with the status.
    private static void ApplyStatusDates(Shipment shipment)
    {
        if (shipment.Status == ShipmentStatus.Delivered)
        {
            shipment.DeliveredDate ??= DateTime.Now;
        }
        else
        {
            shipment.DeliveredDate = null;
        }
    }

    // Format: VP + yyyyMMdd + 4-digit daily sequence, e.g. VP20260610-0007.
    private async Task<string> GenerateTrackingCodeAsync()
    {
        var prefix = $"VP{DateTime.Now:yyyyMMdd}-";
        var next = await _context.Shipments.CountAsync(s => s.TrackingCode.StartsWith(prefix)) + 1;

        // Probe until free — cheap insurance; the unique index is the hard guarantee.
        while (await _context.Shipments.AnyAsync(s => s.TrackingCode == $"{prefix}{next:D4}"))
        {
            next++;
        }

        return $"{prefix}{next:D4}";
    }

    // Dropdown of active transport companies + province typing suggestions for the form.
    private async Task LoadFormDataAsync()
    {
        var companies = await _context.TransportCompanies
            .Where(c => c.IsActive)
            .OrderBy(c => c.Province)
            .ThenBy(c => c.Name)
            .Select(c => new { c.Id, Label = c.Name + " — " + c.Province })
            .ToListAsync();
        ViewBag.Companies = new SelectList(companies, "Id", "Label");

        var companyProvinces = _context.TransportCompanies.Select(c => c.Province);
        var shipmentProvinces = _context.Shipments.Select(s => s.RecipientProvince);
        ViewBag.ProvinceSuggestions = await companyProvinces
            .Union(shipmentProvinces)
            .OrderBy(p => p)
            .ToListAsync();

        // Saved customers feed the sender autofill dropdown on the create form.
        ViewBag.CustomerList = await _context.Customers
            .OrderBy(c => c.Name)
            .ToListAsync();
    }
}
