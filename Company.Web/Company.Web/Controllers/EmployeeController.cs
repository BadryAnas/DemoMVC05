using Company.Data;
using Company.Service.Interfaces;
using Company.Service.Interfaces.Employee.Dto;
using Company.Service.Services;
using Microsoft.AspNetCore.Mvc;

namespace Company.Web.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly IEmployeeService _employeeService;
        private readonly IDepartmentService _departmentService;

        public EmployeeController(IEmployeeService employeeService, IDepartmentService departmentService)
        {
            _employeeService = employeeService;
            _departmentService = departmentService;
        }

        public IActionResult Index(string searchInp)
        {
            IEnumerable<EmployeeDto> employees = new List<EmployeeDto>();

            ViewBag.Message = "Hello From ViewBag";
            ViewData["ViewDataMessage"] = "Hello From ViewData";
            TempData["TempDataMessage"] = "Hello From TempData";

            if (string.IsNullOrEmpty(searchInp))
                employees = _employeeService.GetAll();
            else
                employees = _employeeService.GetByName(searchInp);

            return View(employees);
        }

        public IActionResult Details(int? id,string viewName)
        {
            var employee = _employeeService.GetById(id);
            if (employee is null)
            {
                return RedirectToAction("NotFoundPage", "Home");
            }
            return View( viewName, employee);
        }

        public IActionResult Delete(int? id)
        {
            var employee = _employeeService.GetById(id.Value);
            if (employee is null)
            {
                return RedirectToAction("NotFoundPage", "Home");
            }

            _employeeService.Delete(employee);

            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public IActionResult Create()
        {
            var departments = _departmentService.GetAll();
            ViewBag.Departments = departments;
            return View();
        }

        [HttpPost]
        public IActionResult Create(EmployeeDto employee)
        {
            try
            {
                
                _employeeService.Add(employee);
                return RedirectToAction(nameof(Index));


            }
            catch (Exception ex)
            {
                ModelState.AddModelError("EmployeeError", ex.Message);
                return View(employee);
            }
        }

        [HttpGet]
        public IActionResult Update(int? id)
        {
            return Details(id, "Update");
        }


        [HttpPost]
        public IActionResult Update(int? id , EmployeeDto employee)
        {
            if (id.Value != employee.Id)
            {
                return RedirectToAction("NotFoundPage", "Home");
            }
            _employeeService.Update(employee);

            return RedirectToAction(nameof(Index), "EmployeeDto");
        }

    }
}
