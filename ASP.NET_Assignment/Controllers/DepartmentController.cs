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
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public DepartmentController(IUnitOfWork unitOfWork , IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult Index()
        {
            var departments = _unitOfWork.DepartmentRepository.Value.GetAll();
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
                 _unitOfWork.DepartmentRepository.Value.Add(department);
                var state = _unitOfWork.ApplyToDB();
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

            var department = _unitOfWork.DepartmentRepository.Value.Get(id.Value);
            var empleoyees = _unitOfWork.EmployeeRepositroy.Value.GetAll();
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
                var olddept = _unitOfWork.DepartmentRepository.Value.Get(id.Value);
                var department = _mapper.Map(createDepartmentDto, olddept);
                _unitOfWork.DepartmentRepository.Value.Update(department);
                var state = _unitOfWork.ApplyToDB();
            }
            return RedirectToAction(nameof(Index));
        }
        public IActionResult Delete()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Delete(int? id) {
            var department = _unitOfWork.DepartmentRepository.Value.Get(id.Value);
             _unitOfWork.DepartmentRepository.Value.Delete(department);
            var res = _unitOfWork.ApplyToDB();
            if (res > 0)
            {
                return View("Models/DeletionSuccess");
            }
            return View("Models/DeletionUnSuccess");
        }
    }
}
