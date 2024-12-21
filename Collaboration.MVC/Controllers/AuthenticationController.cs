using Collaboration.Domain.DTOs.Authentication;
using Microsoft.AspNetCore.Mvc;

namespace Collaboration.MVC.Controllers;
public class AuthenticationController : Controller
{
    public IActionResult Login()
    {
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Login(LoginDto loginDto)
    {
        return View();
    }

    public IActionResult Reset()
    {
        return View();
    }
    [ValidateAntiForgeryToken]
    public IActionResult Reset(ResetPasswordDto resetPasswordDto)
    {
        return View();
    }

    public IActionResult Update()
    {
        return View();
    }
    [ValidateAntiForgeryToken]
    public IActionResult Update(UpdatePasswordDto updatePasswordDto)
    {
        return View();
    }
    public IActionResult Lock()
    {
        return View();
    }
    [ValidateAntiForgeryToken]
    public IActionResult Lock(LockPasswordDto lockPasswordDto)
    {
        return View();
    }
}
