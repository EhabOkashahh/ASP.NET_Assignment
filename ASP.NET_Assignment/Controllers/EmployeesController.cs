using ASP.NET.Assignment.PL.DTOs;
using ASP.NET_Assignment.BLL.Interfaces;
using ASP.NET_Assignment.BLL.Repositories;
using ASP.NET_Assignment.DAL.Models;
using AspNetCoreGeneratedDocument;
using Microsoft.AspNetCore.Mvc;
using System.CodeDom;
using System.Threading.Tasks;

namespace ASP.NET.Assignment.PL.Controllers
{
    public class EmployeesController : Controller
    {
        private readonly IEmployeeRepositroy _repositroy;
        private readonly IDepartmentRepository _departmentRepository;

        public EmployeesController(IEmployeeRepositroy employeeRepository, IDepartmentRepository departmentRepository)
        {
            _repositroy = employeeRepository;
            _departmentRepository = departmentRepository;
        }

        public IActionResult Index(string? SearchText)
        {
            IEnumerable<Employee> Employees;

            if (String.IsNullOrEmpty(SearchText)) Employees = _repositroy.GetAll();
            else Employees = _repositroy.GetByName(SearchText);

            return View(Employees);
        }
        
        [HttpGet]
        public IActionResult Create()
        {
            var Departments = _departmentRepository.GetAll();
            ViewData["Departments"] = Departments;
            return View();
        }

        [HttpPost]
        public IActionResult Create(CreateEmployeeDTO createEmployeeDTO)
        {

            if (ModelState.IsValid)
            {
                Employee employee = new Employee()
                {
                    Name = createEmployeeDTO.Name,
                    Address = createEmployeeDTO.Address,
                    Age = createEmployeeDTO.Age,
                    DateOfCreation = createEmployeeDTO.DateOfCreation,
                    Email = createEmployeeDTO.Email,
                    HireDate = createEmployeeDTO.HireDate,
                    IsActive = createEmployeeDTO.IsActive,
                    IsDeleted = createEmployeeDTO.IsDeleted,
                    Phone = createEmployeeDTO.Phone,
                    Salary = createEmployeeDTO.Salary,
                    DepartmentId = createEmployeeDTO.DepartmentId
                };
                var count = _repositroy.Add(employee);
                if (count > 0)
                {
                    return RedirectToAction(nameof(Index));
                }
            }
            return View(createEmployeeDTO);
        }

        public IActionResult Details(int? id)
        {
            var Departments = _departmentRepository.GetAll();
            ViewData["Departments"] = Departments;
            var Employee = _repositroy.Get(id.Value);

            CreateEmployeeDTO EmployeeDTO = new CreateEmployeeDTO()
            {
                Name = Employee.Name,
                Address = Employee.Address,
                Age = Employee.Age,
                DateOfCreation = Employee.DateOfCreation,
                Email = Employee.Email,
                HireDate = Employee.HireDate,
                IsActive = Employee.IsActive,
                IsDeleted = Employee.IsDeleted,
                Phone = Employee.Phone,
                Salary = Employee.Salary,
                DepartmentId = Employee.DepartmentId
            };
            ViewData["Id"] = id.Value;
            return View(EmployeeDTO);
        }
        [HttpGet]
        public IActionResult Edit(int? id)
        {
            return Details(id.Value);
        }

        [HttpPost]
        public IActionResult Edit([FromRoute] int? id , CreateEmployeeDTO createEmployeeDTO)
        {
            if (ModelState.IsValid)
            {
                var Departments = _departmentRepository.GetAll();
                ViewData["Departments"] = Departments;

                Employee employee = new Employee()
                {
                    Id = id.Value,
                    Name = createEmployeeDTO.Name,
                    Address = createEmployeeDTO.Address,
                    Age = createEmployeeDTO.Age,
                    DateOfCreation = createEmployeeDTO.DateOfCreation,
                    Email = createEmployeeDTO.Email,
                    HireDate = createEmployeeDTO.HireDate,
                    IsActive = createEmployeeDTO.IsActive,
                    IsDeleted = createEmployeeDTO.IsDeleted,
                    Phone = createEmployeeDTO.Phone,
                    Salary = createEmployeeDTO.Salary,
                    DepartmentId = createEmployeeDTO.DepartmentId
                };
                var count = _repositroy.Update(employee);
                if (count > 0)
                {
                    return RedirectToAction(nameof(Index));
                }
            }
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Delete([FromRoute] int? id)
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int? id, string deleteOption)
        {


            var employee = _repositroy.Get(id.Value);


            if (deleteOption == "1")
            {
                if (employee.IsActive)
                {
                    employee.IsActive = false;
                    _repositroy.Update(employee);
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    ViewBag.ErrorMessage = "This employee is already deactivated.";
                }
            }
            else if (deleteOption == "2")
            {
                if (!employee.IsDeleted)
                {
                    employee.IsDeleted = true;
                    _repositroy.Update(employee);
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    ViewBag.ErrorMessage = "This employee is already deleted.";
                }
            }

            return View("Delete");
        }



    }
}