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
        var model = new Karyawan { Id = "0" };
        ViewData["Page"] = "add";
        return View("Edit", model);
    }

    public async Task<IActionResult> Edit(string id)
    {
        var employee = await _context.Karyawan.FindAsync(id);
        if (employee == null)
        {
            return NotFound();
        }
        ViewData["Page"] = "edit";
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

        if (Request.Form["Page"] == "add")
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
            // First retrieve the existing record
            var existingEmployee = await _context.Karyawan.FindAsync(employee.Id);

            if (existingEmployee == null)
            {
                // Record not found, perhaps deleted by another user
                return NotFound($"Employee with ID {employee.Id} not found.");
            }

            // Update only the properties you need
            existingEmployee.Name = employee.Name;
            existingEmployee.JoinDate = employee.JoinDate;
            existingEmployee.Age = employee.Age;

            // No need for explicit Update() call
        }

        try
        {
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        catch (DbUpdateConcurrencyException)
        {
            // Handle concurrency conflict
            return View("Edit", employee);
        }
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
