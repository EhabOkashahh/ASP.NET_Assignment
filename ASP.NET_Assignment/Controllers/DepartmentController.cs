using ASP.NET.Assignment.PL.DTOs;
using ASP.NET_Assignment.BLL.Interfaces;
using ASP.NET_Assignment.BLL.Repositories;
using ASP.NET_Assignment.DAL.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.DotNet.Scaffolding.Shared.Messaging;
using Microsoft.EntityFrameworkCore.Query.SqlExpressions;
using Microsoft.EntityFrameworkCore.Update.Internal;

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

        public IActionResult Details(int? id , string ViewName = "Details")
        {
            if (id is null) return BadRequest("Invalid Id");

            var department = _repository.Get(id.Value);
            if (department is null) return NotFound(new {StatusCode = 404 , message = $"Dpeartment With Id {id} not found"});
            CreateDepartmentDto createDepartmentDto = new CreateDepartmentDto()
            {
                Name = department.Name,
                Code= department.Code,
                DateOfCreation= department.DateOfCreation,
            };
            ViewBag.Id = id.Value;
            return View(ViewName,createDepartmentDto);
        }

        [HttpGet]
        public IActionResult Edit(int? id)
        {
            return Details(id,"Edit");
        }
            [HttpPost]
        public IActionResult Edit(int? id , CreateDepartmentDto createDepartmentDto) {
            if (ModelState.IsValid) {  
                Department department = new Department()
                {
                    Code = createDepartmentDto.Code,
                    Name = createDepartmentDto.Name,
                    DateOfCreation =createDepartmentDto.DateOfCreation,
                    Id = id.Value
                };
                _repository.Update(department);
                return RedirectToAction(nameof(Details) , new {id = id.Value});
            }
            ViewBag.Id = id.Value;
            return View(createDepartmentDto);
        }
        public IActionResult Delete()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Delete(int? id) {
            var department = _repository.Get(id.Value);
            var res = _repository.Delete(department);
            if(res > 0)
            {
                return View("DeletionSuccess");
            }
            return View("DeletionUnSuccess");
        }

        public IActionResult Update(int? id)
        {
            return Edit(id.Value);
        }

        [HttpPost]
        public IActionResult Update([FromRoute]int? id , CreateDepartmentDto createDepartmentDto)
        {
            if (ModelState.IsValid)
            {
            Department department = new Department()
            {
                Code = createDepartmentDto.Code,
                Name = createDepartmentDto.Name,
                DateOfCreation = createDepartmentDto.DateOfCreation,
                Id = id.Value
            };
                _repository.Update(department);
                return RedirectToAction(nameof(Index));
            }
            ViewBag.Id = id.Value;
            return View(createDepartmentDto);
        }
    }
}
