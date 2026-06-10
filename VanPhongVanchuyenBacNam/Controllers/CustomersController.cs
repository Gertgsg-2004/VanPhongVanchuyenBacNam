using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using VanPhongVanchuyenBacNam.Data;
using VanPhongVanchuyenBacNam.Models;

namespace VanPhongVanchuyenBacNam.Controllers;

public class CustomersController : Controller
{
    private readonly AppDbContext _context;

    public CustomersController(AppDbContext context)
    {
        _context = context;
    }

    // GET: Customers?search=...
    public async Task<IActionResult> Index(string? search)
    {
        var query = _context.Customers.AsQueryable();

        if (!string.IsNullOrWhiteSpace(search))
        {
            var term = search.Trim();
            query = query.Where(c => c.Name.Contains(term) || c.Phone.Contains(term));
        }

        ViewBag.Search = search;

        var customers = await query.OrderBy(c => c.Name).ToListAsync();
        return View(customers);
    }

    // GET: Customers/Create
    public IActionResult Create()
    {
        return View();
    }

    // POST: Customers/Create
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(Customer customer)
    {
        await ValidatePhoneIsUniqueAsync(customer);
        if (!ModelState.IsValid)
        {
            return View(customer);
        }

        _context.Add(customer);
        await _context.SaveChangesAsync();

        TempData["SuccessMessage"] = $"Đã thêm khách hàng \"{customer.Name}\".";
        return RedirectToAction(nameof(Index));
    }

    // GET: Customers/Edit/5
    public async Task<IActionResult> Edit(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var customer = await _context.Customers.FindAsync(id);
        if (customer == null)
        {
            return NotFound();
        }

        return View(customer);
    }

    // POST: Customers/Edit/5
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, Customer customer)
    {
        if (id != customer.Id)
        {
            return NotFound();
        }

        await ValidatePhoneIsUniqueAsync(customer);
        if (!ModelState.IsValid)
        {
            return View(customer);
        }

        try
        {
            _context.Update(customer);
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!await _context.Customers.AnyAsync(c => c.Id == id))
            {
                return NotFound();
            }
            throw;
        }

        TempData["SuccessMessage"] = $"Đã cập nhật khách hàng \"{customer.Name}\".";
        return RedirectToAction(nameof(Index));
    }

    // GET: Customers/Delete/5
    public async Task<IActionResult> Delete(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var customer = await _context.Customers.FirstOrDefaultAsync(c => c.Id == id);
        if (customer == null)
        {
            return NotFound();
        }

        return View(customer);
    }

    // POST: Customers/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        var customer = await _context.Customers.FindAsync(id);
        if (customer != null)
        {
            _context.Customers.Remove(customer);
            await _context.SaveChangesAsync();
            TempData["SuccessMessage"] = $"Đã xóa khách hàng \"{customer.Name}\".";
        }

        return RedirectToAction(nameof(Index));
    }

    // Friendly validation message before the unique index would reject the save.
    private async Task ValidatePhoneIsUniqueAsync(Customer customer)
    {
        var phoneTaken = await _context.Customers
            .AnyAsync(c => c.Phone == customer.Phone && c.Id != customer.Id);
        if (phoneTaken)
        {
            ModelState.AddModelError(nameof(Customer.Phone), "Số điện thoại này đã có trong danh sách khách quen.");
        }
    }
}
