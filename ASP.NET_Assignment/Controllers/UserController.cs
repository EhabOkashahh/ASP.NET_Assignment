using ASP.NET.Assignment.PL.DTOs;
using ASP.NET_Assignment.DAL.Models;
using AspNetCoreGeneratedDocument;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.Elfie.Model;
using Microsoft.EntityFrameworkCore;
using NuGet.Versioning;

namespace ASP.NET.Assignment.PL.Controllers
{
    public class UserController : Controller
    {
        public UserManager<AppUser> _userManager { get; }
        public SignInManager<AppUser> _signInManager { get; }

        public UserController(UserManager<AppUser> userManager , SignInManager<AppUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }


        public async Task<IActionResult> Index(string? SearchText)
        {
            IEnumerable<UserToReturnDto> users;
            if (String.IsNullOrEmpty(SearchText))
            {
                 users = _userManager.Users.Select(U => new UserToReturnDto()
                {
                    Id = U.Id,
                    UserName = U.UserName,
                    FirstName = U.FirstName,
                    LastName = U.LastName,
                    Email = U.Email,
                    Roles = _userManager.GetRolesAsync(U).Result
                });
            }

            else
            {
                users = _userManager.Users.Select(U => new UserToReturnDto()
                {
                    Id = U.Id,
                    UserName = U.UserName,
                    FirstName = U.FirstName,
                    LastName = U.LastName,
                    Roles = _userManager.GetRolesAsync(U).Result
                }).Where(U=> U.UserName.ToLower().Contains(SearchText.ToLower()));
            }
            ViewBag.LoggedInUser = _userManager.GetUserId(User);
            return View(users);
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

                await _signInManager.SignOutAsync();
                return RedirectToAction(nameof(SignIn),"Account");
            }
            var res = await _userManager.DeleteAsync(userToDelete);


            if (res.Succeeded)
            {
                return View("Models/DeletionSuccess");
            }
            return View("Models/DeletionUnSuccess");
        }
    }
}
