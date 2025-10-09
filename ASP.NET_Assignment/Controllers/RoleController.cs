using ASP.NET.Assignment.PL.DTOs;
using ASP.NET_Assignment.DAL.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace ASP.NET.Assignment.PL.Controllers
{
    [Authorize(Roles = "admin")]
    public class RoleController : Controller
    {
        public RoleManager<IdentityRole> _roleManager { get; }
        public UserManager<AppUser> _userManager { get; }

        public RoleController(RoleManager<IdentityRole> roleManager , UserManager<AppUser> userManager)
        {
            _roleManager = roleManager;
            _userManager = userManager;
        }

        public async Task<IActionResult> Index(string? searchText)
        {
            if (searchText is null)
            {
                var Roles =  _roleManager.Roles.Select(R=> new RoleToReturnDto()
                {
                    Id = R.Id,
                    Name = R.Name,
                });
                return View(Roles);
            }

            var FilteredRoles = _roleManager.Roles.Select(R=> new RoleToReturnDto()
            {
                Id = R.Id,
                Name = R.Name,
            }).Where(R => R.Name.ToLower().Contains(searchText.ToLower()));

            return View(FilteredRoles);
        }

        [HttpGet]
        public async Task<IActionResult> create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> create(RoleToReturnDto model)
        {
            var modelname = await _roleManager.FindByNameAsync(model.Name);
            if(modelname is null)
            {
                IdentityRole role = new()
                {
                    Name = model.Name,
                };
                var res = await _roleManager.CreateAsync(role);
                if (res.Succeeded) return RedirectToAction("Index");
            }
            return View(model);
        }


        [HttpGet]
        public async Task<IActionResult> Details(string? id)
        {
            if (id is null) return View("Error");
            var role = await _roleManager.FindByIdAsync(id);

            if (role is null) return View("Error");
            RoleToReturnDto model = new()
            {
                Id = role.Id,
                Name = role.Name
            };

            // Get all users to display
            var Users = await _userManager.GetUsersInRoleAsync(role.Name);

            
            ViewBag.Users = Users.Select(U=> new UserToReturnDto()
            {
                Id= U.Id,
                UserName = U.UserName,
                FirstName = U.FirstName,
                LastName = U.LastName,
                Email = U.Email
            });

            ViewBag.Id = model.Id;
            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(string? Id)
        {
            return Details(Id).Result;
        }
        [HttpPost]
        public async Task<IActionResult> Edit([FromRoute]string? id , RoleToReturnDto model)
        {
            if (id is null) return View("Error");
            var role = await _roleManager.FindByIdAsync(id);

            if (role is null) return View("Error");

            var rolename = await _roleManager.FindByNameAsync(model.Name);
            if(rolename is null)
            {
                role.Id = model.Id;
                role.Name = model.Name;

                var res = await _roleManager.UpdateAsync(role);
                if (res.Succeeded) return RedirectToAction(nameof(Index));
            }
            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Delete()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Delete(string? id)
        {
            var userToDelete = await _roleManager.FindByIdAsync(id);
            if (userToDelete is null)
            {
                return View("Models/DeletionUnSuccess");
            }
            var res = await _roleManager.DeleteAsync(userToDelete);
            if (res.Succeeded)
            {
                return View("Models/DeletionSuccess");
            }
            return View("Models/DeletionUnSuccess");
        }
    }
}

