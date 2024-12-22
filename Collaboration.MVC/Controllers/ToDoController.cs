using Microsoft.AspNetCore.Mvc;

namespace Collaboration.MVC.Controllers
{
    public class ToDoController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
