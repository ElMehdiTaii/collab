using Collaboration.Domain.Requests;
using Microsoft.AspNetCore.Mvc;

namespace Collaboration.MVC.Controllers;

public class AuthenticationController : Controller
{
    public IActionResult Index()
    {
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Login(AuthenticationRequest authenticationRequest)
    {
        if (string.IsNullOrWhiteSpace(authenticationRequest.Email) ||
            string.IsNullOrWhiteSpace(authenticationRequest.Password))
        {
            return View();
        }
        return View();
    }

    public IActionResult RestPassword()
    {
        //SendResetPasswordMailCommandq
        return View();
    }

    public IActionResult UpdatePassword()
    {
        //ResetPasswordCommand
        return View();
    }
}
