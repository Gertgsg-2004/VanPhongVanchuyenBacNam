using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using VanPhongVanchuyenBacNam.Data;
using VanPhongVanchuyenBacNam.Helpers;
using VanPhongVanchuyenBacNam.Models;

namespace VanPhongVanchuyenBacNam.Controllers;

public class TransportCompaniesController : Controller
{
    private readonly AppDbContext _context;

    public TransportCompaniesController(AppDbContext context)
    {
        _context = context;
    }

    // GET: TransportCompanies?searchName=...&province=...
    public async Task<IActionResult> Index(string? searchName, string? province)
    {
        var query = _context.TransportCompanies.AsQueryable();

        if (!string.IsNullOrWhiteSpace(searchName))
        {
            query = query.Where(c => c.Name.Contains(searchName.Trim()));
        }

        if (!string.IsNullOrWhiteSpace(province))
        {
            query = query.Where(c => c.Province == province);
        }

        ViewBag.Provinces = await GetProvincesAsync();
        ViewBag.SearchName = searchName;
        ViewBag.SelectedProvince = province;

        var companies = await query
            .OrderBy(c => c.Province)
            .ThenBy(c => c.Name)
            .ToListAsync();

        return View(companies);
    }

    // GET: TransportCompanies/Details/5
    public async Task<IActionResult> Details(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var company = await _context.TransportCompanies.FirstOrDefaultAsync(c => c.Id == id);
        if (company == null)
        {
            return NotFound();
        }

        return View(company);
    }

    // GET: TransportCompanies/Create
    public IActionResult Create()
    {
        ViewBag.ProvinceOptions = VietnamProvinces.WithCurrent(null);
        return View();
    }

    // POST: TransportCompanies/Create
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(TransportCompany company)
    {
        if (!ModelState.IsValid)
        {
            ViewBag.ProvinceOptions = VietnamProvinces.WithCurrent(company.Province);
            return View(company);
        }

        _context.Add(company);
        await _context.SaveChangesAsync();

        TempData["SuccessMessage"] = $"Đã thêm nhà xe \"{company.Name}\".";
        return RedirectToAction(nameof(Index));
    }

    // GET: TransportCompanies/Edit/5
    public async Task<IActionResult> Edit(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var company = await _context.TransportCompanies.FindAsync(id);
        if (company == null)
        {
            return NotFound();
        }

        ViewBag.ProvinceOptions = VietnamProvinces.WithCurrent(company.Province);
        return View(company);
    }

    // POST: TransportCompanies/Edit/5
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, TransportCompany company)
    {
        if (id != company.Id)
        {
            return NotFound();
        }

        if (!ModelState.IsValid)
        {
            ViewBag.ProvinceOptions = VietnamProvinces.WithCurrent(company.Province);
            return View(company);
        }

        try
        {
            _context.Update(company);
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            // The record was deleted by someone else while this form was open.
            if (!await _context.TransportCompanies.AnyAsync(c => c.Id == id))
            {
                return NotFound();
            }
            throw;
        }

        TempData["SuccessMessage"] = $"Đã cập nhật nhà xe \"{company.Name}\".";
        return RedirectToAction(nameof(Index));
    }

    // GET: TransportCompanies/Delete/5
    public async Task<IActionResult> Delete(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var company = await _context.TransportCompanies.FirstOrDefaultAsync(c => c.Id == id);
        if (company == null)
        {
            return NotFound();
        }

        return View(company);
    }

    // POST: TransportCompanies/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        var company = await _context.TransportCompanies.FindAsync(id);
        if (company != null)
        {
            _context.TransportCompanies.Remove(company);
            await _context.SaveChangesAsync();
            TempData["SuccessMessage"] = $"Đã xóa nhà xe \"{company.Name}\".";
        }

        return RedirectToAction(nameof(Index));
    }

    // Distinct provinces from existing data, used for the list page filter dropdown.
    private Task<List<string>> GetProvincesAsync()
    {
        return _context.TransportCompanies
            .Select(c => c.Province)
            .Distinct()
            .OrderBy(p => p)
            .ToListAsync();
    }
}
