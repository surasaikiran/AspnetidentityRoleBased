using AspnetidentityRoleBased.Data;
using AspnetidentityRoleBased.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace AspnetidentityRoleBased.Controllers
{
    public class EmployeeController : Controller
    {
        public IConfiguration _configuration { get; }
        ApplicationDbContext _context;
        public EmployeeController(ApplicationDbContext context, IConfiguration configuration)
        {
            _configuration = configuration;
            _context = context;
        }
        public IActionResult Index()
        {
            List<Department> list = _context.department.ToList();
            ViewBag.DepartmentList = new SelectList(list, "DepartmentId", "DepartmentName");

            List<EmployeeViewModelcs> listEmp = _context.employee.Where(x => x.IsDeleted == false)
                .Select(x => new EmployeeViewModelcs
                {
                    Name = x.Name,
                    DepartmentName = x.Department.DepartmentName,
                    Address = x.Address,
                    EmployeeId = x.EmployeeId,
                    City= x.City
                }).ToList();

            return View(listEmp);
        }

        public IActionResult Edit(int id)
        {
            // Fetch the employee data for editing
            EmployeeViewModelcs employee = _context.employee
                .Where(x => x.EmployeeId == id && x.IsDeleted == false)
                .Select(x => new EmployeeViewModelcs
                {
                    Name = x.Name,
                    DepartmentName = x.Department.DepartmentName,
                    Address = x.Address,
                    EmployeeId = x.EmployeeId,
                    City= x.City,
                })
                .FirstOrDefault();

            if (employee == null)
            {
                // Handle not found scenario, maybe redirect to Index or show an error message
                return RedirectToAction("Index");
            }

            // Populate any necessary data (e.g., departments) for the edit view
            List<Department> list = _context.department.ToList();
            ViewBag.DepartmentList = new SelectList(list, "DepartmentId", "DepartmentName");

            return View("Edit", employee); // You need to create an Edit.cshtml view for editing
        }
        [HttpPost]
        public IActionResult Save(EmployeeViewModelcs model)
        {
            if (!ModelState.IsValid)
            {
                try
                {
                    // Perform the update in the database
                    var employeeToUpdate = _context.employee
                        .FirstOrDefault(x => x.EmployeeId == model.EmployeeId && x.IsDeleted == false);

                    if (employeeToUpdate != null)
                    {
                        // Update the properties with the values from the form
                        employeeToUpdate.Name = model.Name;
                        employeeToUpdate.Address = model.Address;
                        employeeToUpdate.City = model.City;
                        //employeeToUpdate.Department.DepartmentName = model.DepartmentName;
                           
                        // Save changes to the database
                        _context.SaveChanges();

                        return RedirectToAction("Index"); // Redirect to the list of employees after successful update
                    }
                }
                catch (Exception ex)
                {
                    // Log the exception or handle it appropriately
                    ModelState.AddModelError(string.Empty, "Error saving changes. Please try again.");
                    // You can log the exception for further investigation
                    // Logger.LogError(ex, "Error saving changes in Save action");
                }
            }

            // If the model is not valid or employee is not found, return to the edit view with the model
            return View("Edit", model);
        }



    }
}
