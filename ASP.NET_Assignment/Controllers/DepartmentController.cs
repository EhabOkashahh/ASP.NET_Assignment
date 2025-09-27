using ASP.NET.Assignment.PL.DTOs;
using ASP.NET_Assignment.BLL.Interfaces;
using ASP.NET_Assignment.BLL.Repositories;
using ASP.NET_Assignment.DAL.Models;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.DotNet.Scaffolding.Shared.Messaging;
using Microsoft.EntityFrameworkCore.Query.SqlExpressions;
using Microsoft.EntityFrameworkCore.Update.Internal;

namespace ASP.NET.Assignment.PL.Controllers
{
    public class DepartmentController : Controller
    {
        private readonly IDepartmentRepository _repository;
        private readonly IEmployeeRepositroy _employeeRepositroy;
        private readonly IMapper _mapper;

        public DepartmentController(IDepartmentRepository departmentRepository , IEmployeeRepositroy employeeRepositroy, IMapper mapper)
        {
            _repository = departmentRepository;
            _employeeRepositroy = employeeRepositroy;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult Index()
        {
            var departments = _repository.GetAll();
            ////Dictionary: => Transfer Extra Information From Controller to view
            //// 1.ViewData
            //ViewData["Message"] = "Hello From ViewData";
            //// 2.ViewBag
            //ViewBag.Message = "Message from View Bag";
            //// 2.TempData
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
                var department = _mapper.Map<Department>(createDepartmentDto);
                var state = _repository.Add(department);
                if (state > 0)
                {
                    TempData["Message"] = $"{department.Name} Department is Successfully Created ";
                    return RedirectToAction(nameof(Index));
                }
            }
            return View(createDepartmentDto);
        }

        public IActionResult Details(int? id)
        {
            if (id is null) return BadRequest("Invalid Id");

            var department = _repository.Get(id.Value);
            var empleoyees = _employeeRepositroy.GetAll();
            ViewData["Employees"] = empleoyees;
            if (department is null) return NotFound(new {StatusCode = 404 , message = $"Dpeartment With Id {id} not found"});

            var createDepartmentDto = _mapper.Map<CreateDepartmentDto>(department);

            ViewBag.Id = id.Value;
            return View(createDepartmentDto);
        }

        [HttpGet]
        public IActionResult Edit(int? id)
        {
            return Details(id.Value);
        }
            [HttpPost]
        public IActionResult Edit([FromRoute]int? id , CreateDepartmentDto createDepartmentDto) {
            if (ModelState.IsValid) {
                var olddept = _repository.Get(id.Value);
                var department = _mapper.Map(createDepartmentDto, olddept);
                _repository.Update(department);
                return RedirectToAction(nameof(Index));
            }
            return RedirectToAction(nameof(Index));
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
                return View("Models/DeletionSuccess");
            }
            return View("Models/DeletionUnSuccess");
        }

    }
}
