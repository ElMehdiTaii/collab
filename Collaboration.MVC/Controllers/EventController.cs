using Microsoft.AspNetCore.Mvc;

namespace Collaboration.MVC.Controllers;

public class EventController : Controller
{
    public IActionResult Index()
    {
        return View();
    }
}
