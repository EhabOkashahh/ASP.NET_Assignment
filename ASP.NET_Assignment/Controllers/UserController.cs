using ASP.NET.Assignment.PL.DTOs;
using ASP.NET_Assignment.DAL.Models;
using AspNetCoreGeneratedDocument;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.Elfie.Model;
using Microsoft.EntityFrameworkCore;
using NuGet.Versioning;

namespace ASP.NET.Assignment.PL.Controllers
{
    [Authorize(Roles = "admin")]
    public class UserController : Controller
    {
        public UserManager<AppUser> _userManager { get; }
        public SignInManager<AppUser> _signInManager { get; }
        public RoleManager<IdentityRole> _roleManager { get; }

        public UserController(UserManager<AppUser> userManager , SignInManager<AppUser> signInManager , RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
        }


        public async Task<IActionResult> Index(string? searchText)
        {
            #region Code
            //var usersQuery = _userManager.Users.AsQueryable();

            //if (!string.IsNullOrEmpty(searchText))
            //{
            //    usersQuery = usersQuery.Where(u => u.UserName.ToLower().Contains(searchText.ToLower()));
            //}

            //var usersList = usersQuery.ToList();
            //var users = new List<UserToReturnDto>();

            //foreach (var user in usersList)
            //{
            //    var roles = await _userManager.GetRolesAsync(user);
            //    users.Add(new UserToReturnDto
            //    {
            //        Id = user.Id,
            //        UserName = user.UserName,
            //        FirstName = user.FirstName,
            //        LastName = user.LastName,
            //        Email = user.Email,
            //        Roles = roles 
            //    });
            //}

            //ViewBag.LoggedInUser = _userManager.GetUserId(User);
            //return View(users); 
            #endregion

            var users = _userManager.Users.AsQueryable();
            if (!String.IsNullOrEmpty(searchText)) users = users.Where(U => U.UserName.ToLower().Contains(searchText.ToLower()));

                var usersToReturn = new List<UserToReturnDto>();
                foreach (var user in users)
                {
                    var Roles = await _userManager.GetRolesAsync(user);
                    usersToReturn.Add(new UserToReturnDto()
                    {
                        Id = user.Id,
                        UserName = user.UserName,
                        FirstName = user.FirstName,
                        LastName = user.LastName,
                        Email = user.Email,
                        Roles = Roles
                    });
                }
            ViewBag.LoggedInUser = _userManager.GetUserId(User);
            return View(usersToReturn);
        }


        public async Task<IActionResult> Details(string? id)
        {
            if (id is null) return BadRequest("Invalid Id");

            var user = _userManager.FindByIdAsync(id).Result;
            if(user is null) return BadRequest("User not Found!");
            var dto = new UserToReturnDto()
            {
                Id = user.Id,
                UserName = user.UserName, 
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                Roles = _userManager.GetRolesAsync(user).Result
            };
            ViewBag.Id = user.Id;
            return View(dto);
        }
        [HttpGet]
        public async Task<IActionResult> Edit(string? id)
        {
            return Details(id).Result;
        }
        [HttpPost]
        public async Task<IActionResult> Edit([FromRoute] string? id, UserToReturnDto model)
        {
            ViewBag.id = id;
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByIdAsync(id);
                if (user is null) return BadRequest("User not Found!");

                user.UserName = model.UserName;
                user.FirstName = model.FirstName;
                user.LastName = model.LastName;
                user.Email = model.Email;
                
                var res = await _userManager.UpdateAsync(user);
                if (res.Succeeded)
                {
                    return RedirectToAction(nameof(Index));
                }
            }
            return RedirectToAction(nameof(Index));
        }
        [HttpGet]
        public IActionResult Delete()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Delete(string? id)
        {
            var userToDelete = await _userManager.FindByIdAsync(id);
            if (userToDelete is null)
            {
                return View("Models/DeletionUnSuccess");
            }
            var currentUserId = _userManager.GetUserId(User);
            if (currentUserId == userToDelete.Id) {

                var deletedRes = await _userManager.DeleteAsync(userToDelete);
                if (deletedRes.Succeeded) { 
                    await _signInManager.SignOutAsync();
                    return RedirectToAction(nameof(SignIn),"Account");
                }
                return View("Models/DeletionUnSuccess");

            }
            var res = await _userManager.DeleteAsync(userToDelete);


            if (res.Succeeded)
            {
                return View("Models/DeletionSuccess");
            }
            return View("Models/DeletionUnSuccess");
        }

        [HttpGet]
        public async Task<IActionResult> AddOrRemoveRole(string? userId)
        {
            #region code
            //var user = await _userManager.FindByIdAsync(userId);
            //if (user == null)
            //    return NotFound();

            //var userRoles = await _userManager.GetRolesAsync(user);
            //var allRoles = _roleManager.Roles.ToList();

            //var model = new EditUserRolesViewModel
            //{
            //    UserId = user.Id,
            //    UserName = user.UserName,
            //    Email = user.Email,
            //    Roles = allRoles.Select(role => new UserRolesDto
            //    {
            //        RoleId = role.Id,
            //        RoleName = role.Name,
            //        IsSelected = userRoles.Contains(role.Name)
            //    }).ToList()
            //};
            //return View(model); 
            #endregion
            var user = await _userManager.FindByIdAsync(userId);
            if(user is null) return View("Error");

            var userRoles =await _userManager.GetRolesAsync(user);
            var AllRoles =await _roleManager.Roles.ToListAsync();

            var model = new EditUserRolesViewModel()
            {
                UserId = user.Id,
                UserName = user.UserName,
                Email = user.Email,
                Roles = AllRoles.Select(R => new UserRolesDto()
                {
                    RoleId = R.Id,
                    RoleName = R.Name,
                    IsSelected = userRoles.Contains(R.Name)
                }).ToList()
            };

            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> AddOrRemoveRole(EditUserRolesViewModel model)
        {
            var user = await _userManager.FindByIdAsync(model.UserId);
            if (user is null) return View("Error");

            var CurrentRoles = await _userManager.GetRolesAsync(user);
            var SelectedRoles = model.Roles.Where(R => R.IsSelected).Select(R => R.RoleName);

            await _userManager.AddToRolesAsync(user, SelectedRoles.Except(CurrentRoles));

            await _userManager.RemoveFromRolesAsync(user, CurrentRoles.Except(SelectedRoles));

            return RedirectToAction(nameof(Index));
        }
    }
}
    