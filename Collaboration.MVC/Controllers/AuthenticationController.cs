using AutoMapper;
using Collaboration.Application.Features.Authentication.Commands.ResetPasswordCommand;
using Collaboration.Application.Features.Authentication.Commands.SendResetPasswordMailCommand;
using Collaboration.Application.Features.Authentication.Queries.AuthenticationQuery;
using Collaboration.Domain.DTOs.Authentication;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Collaboration.MVC.Controllers;
public class AuthenticationController(IMapper _mapper, IMediator _mediator) : Controller
{
    public IActionResult Login()
    {
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Login(LoginDto loginDto)
    {
        try
        {
            var query = _mapper.Map<AuthenticationQuery>(loginDto);
            var result = await _mediator.Send(query);
            return RedirectToAction("Index", "Dashboard");
        }
        catch (Exception ex)
        {
            ViewBag.ErrorMessage = ex.Message;
            return View(loginDto);
        }
    }

    public IActionResult Reset()
    {
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Reset(ResetPasswordDto resetPasswordDto)
    {
        try
        {
            var command = _mapper.Map<SendResetPasswordCommand>(resetPasswordDto);
            var result = await _mediator.Send(command);
            return RedirectToAction("Success");
        }
        catch (Exception ex)
        {
            ViewBag.ErrorMessage = ex.Message;
            return View(resetPasswordDto);
        }
    }

    public IActionResult Update()
    {
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Update(UpdatePasswordDto updatePasswordDto)
    {
        try
        {
            var command = _mapper.Map<ResetPasswordCommand>(updatePasswordDto);
            var result = await _mediator.Send(command);
            return RedirectToAction("Login");
        }
        catch (Exception ex)
        {
            ViewBag.ErrorMessage = ex.Message;
            return View(updatePasswordDto);
        }
    }

    public IActionResult Lock()
    {
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Lock(LockPasswordDto lockPasswordDto)
    {
        return View();
    }

    public IActionResult Success()
    {
        return View();
    }

    [Authorize]
    public IActionResult Logout()
    {
        return View();
    }
}
