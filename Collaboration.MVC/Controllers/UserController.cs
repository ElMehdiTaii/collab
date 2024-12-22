using Microsoft.AspNetCore.Mvc;

namespace Collaboration.MVC.Controllers
{
    public class UserController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
