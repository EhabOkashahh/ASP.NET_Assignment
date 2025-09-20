using ASP.NET.Assignment.PL.DTOs;
using ASP.NET_Assignment.BLL.Interfaces;
using ASP.NET_Assignment.BLL.Repositories;
using ASP.NET_Assignment.DAL.Models;
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
        [HttpPost]
        public IActionResult Create(CreateDepartmentDto createDepartmentDto) {

            if (ModelState.IsValid)
            {
                Department department = new Department()
                {
                    Code = createDepartmentDto.Code,
                    Name = createDepartmentDto.Name,
                    DateOfCreation = createDepartmentDto.DateOfCreation,
                };
                var state = _repository.Add(department);
                if (state > 0)
                {
                    return RedirectToAction(nameof(Index));
                }
            }
            return View(createDepartmentDto);
        }

        public IActionResult Details() { 
        

            return View();
        }
    }
}
