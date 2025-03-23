using Microsoft.AspNetCore.Mvc;
using my_test_net.Models;

namespace my_test_net.Controllers;

public class EmployeeController : Controller
{
    // Temporary storage for employees - in a real app, use a database
    private static List<Employee> _employees = new List<Employee>
    {
        new Employee { Id = 1, Name = "Budi Santoso", JoinDate = DateTime.Parse("2020-01-15"), Age = 28 },
        new Employee { Id = 2, Name = "Siti Rahayu", JoinDate = DateTime.Parse("2019-05-20"), Age = 32 },
        new Employee { Id = 3, Name = "Ahmad Wijaya", JoinDate = DateTime.Parse("2021-03-10"), Age = 25 }
    };
    
    private static int _nextId = 4;
    
    public IActionResult Index()
    {
        return View(_employees);
    }
    
    public IActionResult Create()
    {
        return View("Edit", new Employee { Id = 0 });
    }
    
    public IActionResult Edit(int id)
    {
        var employee = _employees.FirstOrDefault(e => e.Id == id);
        if (employee == null)
        {
            return NotFound();
        }
        return View(employee);
    }
    
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Save(Employee employee)
    {
        if (!ModelState.IsValid)
        {
            return View("Edit", employee);
        }
        
        if (employee.Id == 0)
        {
            // New employee
            employee.Id = _nextId++;
            _employees.Add(employee);
        }
        else
        {
            // Update existing employee
            var index = _employees.FindIndex(e => e.Id == employee.Id);
            if (index != -1)
            {
                _employees[index] = employee;
            }
        }
        
        return RedirectToAction(nameof(Index));
    }
    
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Delete(int id)
    {
        var employee = _employees.FirstOrDefault(e => e.Id == id);
        if (employee != null)
        {
            _employees.Remove(employee);
        }
        
        return RedirectToAction(nameof(Index));
    }
}
