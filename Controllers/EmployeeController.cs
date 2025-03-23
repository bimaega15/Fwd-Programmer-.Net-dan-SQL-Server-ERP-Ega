using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using my_test_net.Data;
using my_test_net.Models;

namespace my_test_net.Controllers;

public class EmployeeController : Controller
{
    private readonly ApplicationDbContext _context;
    private readonly DateTime _minSqlDate = new DateTime(1753, 1, 1);

    public EmployeeController(ApplicationDbContext context)
    {
        _context = context;
    }
    
    public async Task<IActionResult> Index()
    {
        return View(await _context.Karyawan.ToListAsync());
    }
    
    public IActionResult Create()
    {
        return View("Edit", new Karyawan { Id = "0" });
    }
    
    public async Task<IActionResult> Edit(string id)
    {
        var employee = await _context.Karyawan.FindAsync(id);
        if (employee == null)
        {
            return NotFound();
        }
        return View(employee);
    }
    
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Save(Karyawan employee)
    {
        // Validate date is within SQL Server datetime range
        if (employee.JoinDate < _minSqlDate)
        {
            ModelState.AddModelError("JoinDate", "Tanggal tidak valid. Harus setelah 1 Januari 1753.");
            return View("Edit", employee);
        }

        if (!ModelState.IsValid)
        {
            return View("Edit", employee);
        }
        
        if (employee.Id == "0" || string.IsNullOrEmpty(employee.Id))
        {
            // New employee
            // Ensure Id is not null
            if (string.IsNullOrEmpty(employee.Id))
            {
                ModelState.AddModelError("Id", "ID Karyawan tidak boleh kosong");
                return View("Edit", employee);
            }
            
            _context.Add(employee);
        }
        else
        {
            // Update existing employee
            // Ensure Id is not null
            if (string.IsNullOrEmpty(employee.Id))
            {
                ModelState.AddModelError("Id", "ID Karyawan tidak boleh kosong");
                return View("Edit", employee);
            }
            _context.Update(employee);
        }
        
        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }
    
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Delete(string id)
    {
        var employee = await _context.Karyawan.FindAsync(id);
        if (employee != null)
        {
            _context.Karyawan.Remove(employee);
            await _context.SaveChangesAsync();
        }
        
        return RedirectToAction(nameof(Index));
    }
}
