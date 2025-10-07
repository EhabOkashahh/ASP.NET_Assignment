using ASP.NET.Assignment.PL.DTOs;
using ASP.NET_Assignment.Controllers;
using ASP.NET_Assignment.DAL.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Data.Common;

namespace ASP.NET.Assignment.PL.Controllers
{
    public class AccountController : Controller
    {
        public UserManager<AppUser> _userManager { get; }
        public SignInManager<AppUser> _signInManager { get; }

        public AccountController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }


        #region SignUp
        [HttpGet]
        public IActionResult SignUp()
        {
            return View();
        }
        //P@ssW0rd

        [HttpPost]
        public async Task<IActionResult> SignUp(SignUpDto model)
        {
            if (ModelState.IsValid)
            {

               var user = await _userManager.FindByNameAsync(model.UserName);
                if (user == null) {
                    user = await _userManager.FindByEmailAsync(model.Email);
                    if (user == null) {
                        user = new AppUser()
                        {
                            UserName = model.UserName,
                            FirstName = model.FirstName,
                            LastName = model.LastName,
                            Email = model.Email,
                            IsAgree = model.IsAgree,
                        };
                        var result = await _userManager.CreateAsync(user, model.Password);
                        if (result.Succeeded) return RedirectToAction(nameof(SignIn));
                        foreach (var err in result.Errors)
                        {
                            ModelState.AddModelError("", err.Description);
                        }
                    }

                }
                    ModelState.AddModelError("", "This Username or Email is used before");
            }
            return View(model);
        }


        #endregion

        #region SignIn
        [HttpGet]
        public IActionResult SignIn()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> SignIn(SignInDto model)
        {
            if (ModelState.IsValid) {
                var user = await _userManager.FindByEmailAsync(model.Email);
                if(user is not null)
                {
                    bool pass = await _userManager.CheckPasswordAsync(user, model.Password);
                    if (pass)
                    {
                        var res = await _signInManager.PasswordSignInAsync(user,model.Password,model.RememberMe,false);
                        if (res.Succeeded)
                        {
                            return RedirectToAction(nameof(HomeController.Index),"Home");
                        }
                    }
                }


                ModelState.AddModelError("", "Wrong Email or Password!");
            }
            return View(model);
        }

        #endregion

        #region SignOut
        [HttpGet]
        public async Task<IActionResult> SignOut()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction(nameof(SignIn),"Account");
        }
        #endregion
    }
}

