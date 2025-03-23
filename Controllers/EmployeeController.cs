using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using my_test_net.Data;
using my_test_net.Models;

namespace my_test_net.Controllers;

public class EmployeeController : Controller
{
    private readonly ApplicationDbContext _context;

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
        return View("Edit", new Karyawan { Id = 0 });
    }
    
    public async Task<IActionResult> Edit(int id)
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
        if (!ModelState.IsValid)
        {
            return View("Edit", employee);
        }
        
        if (employee.Id == 0)
        {
            // New employee
            _context.Add(employee);
        }
        else
        {
            // Update existing employee
            _context.Update(employee);
        }
        
        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }
    
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Delete(int id)
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
