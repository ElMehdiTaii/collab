using Collaboration.Application.Features.Authentication.Queries.AuthenticationQuery;
using Collaboration.Domain.Requests;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Collaboration.MVC.Controllers;

public class AuthenticationController(IMediator _mediator) : Controller
{
    public IActionResult Index()
    {
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async IActionResult Login(AuthenticationRequest authenticationRequest)
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
