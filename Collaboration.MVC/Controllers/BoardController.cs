using Microsoft.AspNetCore.Mvc;

namespace Collaboration.MVC.Controllers
{
    public class BoardController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
