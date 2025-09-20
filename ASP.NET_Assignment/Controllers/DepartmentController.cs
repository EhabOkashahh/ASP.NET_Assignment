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
        [HttpGet]
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

        public IActionResult Details(int id)
        {
            var department = _repository.Get(id);
            return View(department);
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            var department = _repository.Get(id);
            return View(department);
        }

        [HttpPost]
        public IActionResult Edit(Department department) {
            if (ModelState.IsValid) {  
                _repository.Update(department);
                return RedirectToAction(nameof(Details) , new {id = department.Id});
            }
            return View(department);
        }
        public IActionResult Delete()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Delete(int id) {
            var department = _repository.Get(id);
            var res = _repository.Delete(department);
            if(res > 0)
            {
                return View("DeletionSuccess");
            }
            return View("DeletionUnSuccess");
        }
    }
}
