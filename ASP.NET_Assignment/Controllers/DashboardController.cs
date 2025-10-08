using Microsoft.AspNetCore.Mvc;

namespace ASP.NET.Assignment.PL.Controllers
{
    public class DashboardController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
