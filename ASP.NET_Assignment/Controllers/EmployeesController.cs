using ASP.NET.Assignment.PL.DTOs;
using ASP.NET_Assignment.BLL.Interfaces;
using ASP.NET_Assignment.BLL.Repositories;
using ASP.NET_Assignment.DAL.Models;
using AspNetCoreGeneratedDocument;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System.CodeDom;
using System.Threading.Tasks;

namespace ASP.NET.Assignment.PL.Controllers
{
    public class EmployeesController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _Mapper;

        public EmployeesController(IUnitOfWork unitOfWork,IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _Mapper = mapper;
        }

        public IActionResult Index(string? SearchText)
        {
            IEnumerable<Employee> Employees;

            if (String.IsNullOrEmpty(SearchText)) Employees = _unitOfWork.EmployeeRepositroy.Value.GetAll();
            else Employees = _unitOfWork.EmployeeRepositroy.Value.GetByName(SearchText);

            return View(Employees);
        }
        [HttpGet]
        public IActionResult Create()
        {
            var Departments = _unitOfWork.DepartmentRepository.Value.GetAll();
            ViewData["Departments"] = Departments;
            return View();
        }

        [HttpPost]
        public IActionResult Create(CreateEmployeeDTO createEmployeeDTO)
        {

            if (ModelState.IsValid)
            {
               
                var employee = _Mapper.Map<Employee>(createEmployeeDTO);
                 _unitOfWork.EmployeeRepositroy.Value.Add(employee);
                var count = _unitOfWork.ApplyToDB();
                if (count > 0)
                {
                    return RedirectToAction(nameof(Index));
                }
            }
            return View(createEmployeeDTO);
        }

        public IActionResult Details(int? id)
        {
            var Departments = _unitOfWork.DepartmentRepository.Value.GetAll();
            ViewData["Departments"] = Departments;
            var Employee = _unitOfWork.EmployeeRepositroy.Value.Get(id.Value);
            var employee = _Mapper.Map<CreateEmployeeDTO>(Employee);

            ViewData["Id"] = id.Value;
            return View(employee);
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
                var Departments = _unitOfWork.DepartmentRepository.Value.GetAll();
                ViewData["Departments"] = Departments;
                var oldemp = _unitOfWork.EmployeeRepositroy.Value.Get(id.Value);

               var employee = _Mapper.Map(createEmployeeDTO , oldemp);

                _unitOfWork.EmployeeRepositroy.Value.Update(employee);
                _unitOfWork.ApplyToDB();
            }
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Delete([FromRoute] int? id)
        {
            return View();
        }

        [HttpPost]
        public IActionResult Delete(int? id, string deleteOption)
        {


            var employee = _unitOfWork.EmployeeRepositroy.Value.Get(id.Value);


            if (deleteOption == "1")
            {
                if (employee.IsActive)
                {
                    employee.IsActive = false;
                    _unitOfWork.EmployeeRepositroy.Value.Update(employee);
                    var count = _unitOfWork.ApplyToDB();
                    if(count > 0) return RedirectToAction(nameof(Index));
                    {
                        ViewBag.ErrorMessage = "Something Wrong Happend";
                        return Delete(id);
                    }
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
                    _unitOfWork.EmployeeRepositroy.Value.Update(employee);
                    var count = _unitOfWork.ApplyToDB();
                    if (count > 0) return RedirectToAction(nameof(Index));
                    {
                        ViewBag.ErrorMessage = "Something Wrong Happend";
                        return Delete(id);
                    }
                }
                else
                {
                    ViewBag.ErrorMessage = "This employee is already deleted.";
                }
            }
            else ViewBag.ErrorMessage = "Please Choose Deletion Method.";

            return View("Delete");
        }

    }
}