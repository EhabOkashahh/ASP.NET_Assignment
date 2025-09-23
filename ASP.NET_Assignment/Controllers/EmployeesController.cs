using ASP.NET.Assignment.PL.DTOs;
using ASP.NET_Assignment.BLL.Interfaces;
using ASP.NET_Assignment.BLL.Repositories;
using ASP.NET_Assignment.DAL.Models;
using Microsoft.AspNetCore.Mvc;
using System.CodeDom;

namespace ASP.NET.Assignment.PL.Controllers
{
    public class EmployeesController : Controller
    {
        private readonly IEmployeeRepositroy _repositroy;
        public EmployeesController(IEmployeeRepositroy employeeRepository)
        {
            _repositroy = employeeRepository;
        }

        public IActionResult Index()
        {
            var Employees = _repositroy.GetAll();
            return View(Employees);
        }
        [HttpGet]
        public IActionResult Create() {
        
            return View();
        }
        [HttpPost]
        public IActionResult Create(CreateEmployeeDTO createEmployeeDTO) {

            if (ModelState.IsValid)
            {
                Employee employee = new Employee()
                {
                    Age = createEmployeeDTO.Age,
                    Address= createEmployeeDTO.Address,
                    DateOfCreation = createEmployeeDTO.DateOfCreation,
                    Email = createEmployeeDTO.Email,
                    HireDate = createEmployeeDTO.HireDate,
                    IsActive = createEmployeeDTO.IsActive,
                    IsDeleted = createEmployeeDTO.IsDeleted,
                    Name = createEmployeeDTO.Name,
                    Phone = createEmployeeDTO.Phone,
                    Salary = createEmployeeDTO.Salary,
                };
                var count = _repositroy.Add(employee);
                if(count > 0)
                {
                    return RedirectToAction(nameof(Index));
                }
            }
            return View(createEmployeeDTO);
        }

        public IActionResult Details(int? id)
        {
            var Employee = _repositroy.Get(id.Value);
            return View(Employee);
        }
        [HttpGet]
        public IActionResult Edit(int? id)
        {
            var Employee = _repositroy.Get(id.Value);
            CreateEmployeeDTO createDepartmentDto = new CreateEmployeeDTO()
            {
                Age = Employee.Age,
                Address = Employee.Address,
                DateOfCreation = Employee.DateOfCreation,
                Email = Employee.Email,
                HireDate = Employee.HireDate,
                IsActive = Employee.IsActive,
                IsDeleted = Employee.IsDeleted,
                Name = Employee.Name,
                Phone = Employee.Phone,
                Salary = Employee.Salary,
            };
            ViewBag.Id = id.Value;
            return View(createDepartmentDto);
        }
        [HttpPost]
        public IActionResult Edit([FromRoute]int? id , CreateEmployeeDTO createEmployeeDTO)
        {
            if (ModelState.IsValid) { 
                Employee employee = new Employee()
                {
                    Id = id.Value,
                    Age = createEmployeeDTO.Age,
                    Address = createEmployeeDTO.Address,
                    DateOfCreation = createEmployeeDTO.DateOfCreation,
                    Email = createEmployeeDTO.Email,
                    HireDate = createEmployeeDTO.HireDate,
                    IsActive = createEmployeeDTO.IsActive,
                    IsDeleted = createEmployeeDTO.IsDeleted,
                    Name = createEmployeeDTO.Name,
                    Phone = createEmployeeDTO.Phone,
                    Salary = createEmployeeDTO.Salary,
                };

                var res = _repositroy.Update(employee);
                if(res >= 0) return RedirectToAction(nameof(Details), new {id = id.Value});
                else BadRequest("Something Wrong Happen");
            }
            ViewBag.Id = id.Value;
            return View(createEmployeeDTO);
        }
    }
}
