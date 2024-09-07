using Company.Data;
using Company.Repository.Interfaces;
using Company.Repository.Repositories;
using Company.Service.Interfaces;
using Company.Service.Interfaces.Department.Dto;
using Microsoft.AspNetCore.Mvc;

namespace Company.Web.Controllers
{
    public class DepartmentController : Controller
    {
        private readonly IDepartmentService _departmentService;

        public DepartmentController(IDepartmentService departmentService)
        {
            _departmentService = departmentService;
        }


        public IActionResult Index()
        {
            var department = _departmentService.GetAll();
            return View(department);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(DepartmentDto department)
        {
            try
            {

                if (ModelState.IsValid)
                {
                    _departmentService.Add(department);


                    //TempData["TempDataMessage"] = "Hello From TempData";

                    return RedirectToAction(nameof(Index));

                    //return Redirect(nameof(Index));
                }

                return View(department);

            }
            catch (Exception ex) 
            {
                ModelState.AddModelError("DepartmentError",ex.Message);
                return View(department);
            }
        }

        public IActionResult Details(int? id, string viewName = "Details")
        {
            var department =  _departmentService.GetById(id);

            if (department is null)
            {
                return RedirectToAction("NotFoundPage", "Home");
            }
            return View(viewName,department);
        }

        [HttpGet]
        public IActionResult Update(int? id)
        {
            return Details(id, "Update");
        }

        [HttpPost]
        public IActionResult Update(int? id , DepartmentDto department)
        {
            if (id.Value != department.Id)
            {
                return RedirectToAction("NotFoundPage", "Home");
            }
            _departmentService.Update(department);

            return RedirectToAction(nameof(Index), "DepartmentDto");
        }

        public IActionResult Delete(int? id)
        {
            var department = _departmentService.GetById(id);

            if (department is null)
            {
                return RedirectToAction("NotFoundPage", "Home");
            }

            _departmentService.Delete(department);

            return RedirectToAction(nameof(Index));
        }

    }
}
