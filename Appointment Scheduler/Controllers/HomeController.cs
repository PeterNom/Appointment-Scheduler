using Microsoft.AspNetCore.Mvc;

namespace Appointment_Scheduler.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
