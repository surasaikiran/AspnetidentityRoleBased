using AspnetidentityRoleBased.Data;
using AspnetidentityRoleBased.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace AspnetidentityRoleBased.Controllers
{
    public class HomeController1 : Controller
    {
        public IConfiguration _configuration { get; }
        ApplicationDbContext _context;
        public HomeController1(ApplicationDbContext context, IConfiguration configuration)
        {
            _configuration = configuration;
            _context = context;
        }
        public IActionResult Index()
        {
            return View();
        }
        public ActionResult Employee()
        {
            List<Department> list = _context.department.ToList();
            ViewBag.DepartmentList = new SelectList(list, "DepartmentId", "DepartmentName");

            List<EmployeeViewModelcs> listEmp = _context.employee.Where(x => x.IsDeleted == false)
                .Select(x => new EmployeeViewModelcs
                {
                    Name = x.Name,
                    DepartmentName = x.Department.DepartmentName,
                    Address = x.Address,
                    EmployeeId = x.EmployeeId
                }).ToList();
            return View(listEmp);
        }
        [HttpPost]
        public IActionResult Employee(EmployeeViewModelcs model)
        {
            try
            {
                List<Department> list = _context.department.ToList();
                ViewBag.DepartmentList = new SelectList(list, "DepartmentId", "DepartmentName");

                if (model.EmployeeId > 0)
                {
                    //update
                    Employee emp = _context.employee.SingleOrDefault(x => x.EmployeeId == model.EmployeeId && x.IsDeleted == false);

                    emp.DepartmentId = model.DepartmentId;
                    emp.Name = model.Name;
                    emp.Address = model.Address;
                    _context.SaveChanges();


                }
                else
                {
                    //Insert
                    Employee emp = new Employee();
                    emp.Address = model.Address;
                    emp.Name = model.Name;
                    emp.DepartmentId = model.DepartmentId;
                    emp.IsDeleted = false;
                    _context.employee.Add(emp);
                    _context.SaveChanges();

                }
                return View(model);

            }
            catch (Exception ex)
            {

                throw ex;
            }

        }
        public ActionResult AddEditEmployee(int EmployeeId)
        {
            List<Department> list = _context.department.ToList();
            ViewBag.DepartmentList = new SelectList(list, "DepartmentId", "DepartmentName");

            EmployeeViewModelcs model = new EmployeeViewModelcs();

            if (EmployeeId >= 0)
            {

                Employee emp = _context.employee.SingleOrDefault(x => x.EmployeeId == EmployeeId && x.IsDeleted == false);
                model.EmployeeId = emp.EmployeeId;
                model.DepartmentId = emp.DepartmentId;
                model.Name = emp.Name;
                model.Address = emp.Address;

            }
            return PartialView("_AddEditUpdate", model);
        }
    }
}
