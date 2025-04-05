using Microsoft.AspNetCore.Mvc;

namespace Collaboration.MVC.Controllers
{
    public class CalendarController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Get()
        {
            return View();
        }
    }
}
