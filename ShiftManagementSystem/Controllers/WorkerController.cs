using Microsoft.AspNetCore.Mvc;

namespace ShiftManagementSystem.Controllers
{
    public class WorkerController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

    }
}
