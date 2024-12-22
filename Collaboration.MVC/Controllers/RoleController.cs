using Microsoft.AspNetCore.Mvc;

namespace Collaboration.MVC.Controllers
{
    public class RoleController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
