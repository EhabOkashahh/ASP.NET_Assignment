using ASP.NET.Assignment.PL.DTOs;
using ASP.NET.Assignment.PL.Helpers;
using ASP.NET_Assignment.DAL.Models;
using ASP.NET_Assignment.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Threading.Tasks;

namespace ASP.NET_Assignment.Controllers
{
    [RequiredRoleLevel(1)]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public UserManager<AppUser> _userManager { get; }
        public RoleManager<AppRole> _roleManager { get; }
        public RoleService RoleService { get; }

        public HomeController(ILogger<HomeController> logger , UserManager<AppUser> userManager , RoleManager<AppRole> roleManager , RoleService roleService)
        {
            _logger = logger;
            _userManager = userManager;
            _roleManager = roleManager;
            RoleService = roleService;
        }

        public async Task<IActionResult> Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
