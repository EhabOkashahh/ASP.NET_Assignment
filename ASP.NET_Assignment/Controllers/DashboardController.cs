using ASP.NET.Assignment.PL.Helpers;
using ASP.NET_Assignment.Controllers;
using ASP.NET_Assignment.DAL.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace ASP.NET.Assignment.PL.Controllers
{
    [RequiredRoleLevel(3)]
    public class DashboardController : Controller
    {
        public UserManager<AppUser> _userManager { get; }
        public RoleManager<AppRole> _roleManager { get; }
        public RoleService RoleService { get; }

        public DashboardController(UserManager<AppUser> userManager, RoleManager<AppRole> roleManager, RoleService roleService)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            RoleService = roleService;
        }
        public async Task<IActionResult> Index()
        { 
                return View();
                
        }
    }
}
