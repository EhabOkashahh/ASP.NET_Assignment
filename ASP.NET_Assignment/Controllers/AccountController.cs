using ASP.NET.Assignment.PL.DTOs;
using ASP.NET.Assignment.PL.Helpers;
using ASP.NET.Assignment.PL.Helpers.Services.Mail;
using ASP.NET_Assignment.Controllers;
using ASP.NET_Assignment.DAL.Models;
using ASP.NET_Assignment.Models;
using MailKit;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages;
using Newtonsoft.Json.Linq;
using System.Data.Common;
using System.Threading.Tasks;

namespace ASP.NET.Assignment.PL.Controllers
{
    public class AccountController : Controller
    {
        public UserManager<AppUser> _userManager { get; }
        public SignInManager<AppUser> _signInManager { get; }

        public readonly IMailServices _mailService;

        public AccountController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, IMailServices mailService)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _mailService = mailService;
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
                            ImageName = "DefaultPFP.png"
                        };
                        var result = await _userManager.CreateAsync(user, model.Password);
                        await _userManager.AddToRoleAsync(user  , "Member");
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

        #region Forget Password
        [HttpGet]
        public IActionResult ForgetPassword () {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> SendResetEmailURL (ForgetPasswordDto model) 
        {

            if (ModelState.IsValid) {
                var user = await _userManager.FindByEmailAsync(model.Email);
                if(user is null)
                {
                    ModelState.AddModelError("", "Invalid Email!");
                    return View(nameof(ForgetPassword),model);
                    
                }
                //Generate token
                var token = await _userManager.GeneratePasswordResetTokenAsync(user);
                //Generate URL
                var url = Url.Action("ResetPasword","Account", new { email = model.Email, token } , Request.Scheme);


                var Email = new Email()
                {
                    To = model.Email,
                    subject = "Reset Password",
                    Body = url
                };
                var flag = _mailService.SendEmail(Email);
                if (!flag) ModelState.AddModelError("", "Something Wrong happened try again later");
                else return View("CheckInbox");

            }
            
            return View(nameof(ForgetPassword),model);
        }

        #endregion

        #region Reset Password
        [HttpGet]
        public IActionResult ResetPasword(string email , string token)
        {
            TempData["email"] = email; 
            TempData["token"] = token;
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> ResetPasword(ResetPaswordDto model)
        {
            if (ModelState.IsValid) {
                var email = TempData["email"] as string;
                var token = TempData["token"] as string;
                if (email is null || token is null) return View("Error");
                var user =await _userManager.FindByEmailAsync(email);

                if(user is null) return View("Error");
                var res = await _userManager.ResetPasswordAsync(user,token,model.Password);

                if (!res.Succeeded) return View("Error");
                return RedirectToAction(nameof(SignIn));
            }
            return View(model);
        }
        #endregion
    }
}

