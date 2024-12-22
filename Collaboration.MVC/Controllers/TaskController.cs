using Microsoft.AspNetCore.Mvc;

namespace Collaboration.MVC.Controllers
{
    public class TaskController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
