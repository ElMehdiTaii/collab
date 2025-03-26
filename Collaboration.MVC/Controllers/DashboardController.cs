using Microsoft.AspNetCore.Mvc;

namespace Collaboration.MVC.Controllers;

public class DashboardController : Controller
{
    public IActionResult Index()
    {
        return View();
    }
}
