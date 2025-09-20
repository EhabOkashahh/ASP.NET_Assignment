using ASP.NET_Assignment.BLL.Interfaces;
using ASP.NET_Assignment.BLL.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace ASP.NET.Assignment.PL.Controllers
{
    public class DepartmentController : Controller
    {
        private readonly IDepartmentRepository _repository;
        public DepartmentController(IDepartmentRepository departmentRepository)
        {
            _repository = departmentRepository;
        }

        [HttpGet]
        public IActionResult Index()
        {
            var departments = _repository.GetAll();
            return View(departments);
        }

        public IActionResult Create() {
            return View();
        }
    }
}
