using Microsoft.AspNetCore.Mvc;

namespace Collaboration.MVC.Controllers;

public class TaskAttachmentController : Controller
{
    public IActionResult Get(int id)
    {
        return View();
    }
    public IActionResult GetAll(int taskId)
    {
        return View();
    }
    public IActionResult Delete(int id)
    {
        return View();
    }
    public IActionResult Create()
    {
        return View();
    }
}
