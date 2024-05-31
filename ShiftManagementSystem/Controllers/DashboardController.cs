using Microsoft.AspNetCore.Mvc;

namespace ShiftManagementSystem.Controllers
{
    public class DashboardController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
