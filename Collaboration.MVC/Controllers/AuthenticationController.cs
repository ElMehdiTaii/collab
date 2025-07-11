﻿using AutoMapper;
using Collaboration.Application.Exceptions;
using Collaboration.Application.Features.Authentication.Commands.ResetPasswordCommand;
using Collaboration.Application.Features.Authentication.Commands.SendResetPasswordMailCommand;
using Collaboration.Application.Features.Authentication.Queries.AuthenticationQuery;
using Collaboration.Domain.DTOs.Authentication;
using Collaboration.Domain.DTOs.User;
using MediatR;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

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
            var userInfo = _mapper.Map<GetUserDto>(result.Data);
            await SetUpCookiesAsync(userInfo, loginDto.RememberMe);
            return RedirectToAction("Index", "Dashboard");
        }
        catch (BadRequestException ex)
        {
            ViewBag.ErrorMessage = ex.Message;
            return View(loginDto);
        }
        catch (Exception)
        {
            return RedirectToAction("Index", "Error");
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
        catch (BadRequestException ex)
        {
            ViewBag.ErrorMessage = ex.Message;
            return View(resetPasswordDto);
        }
        catch (Exception)
        {
            return RedirectToAction("Index", "Error");
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
        catch (BadRequestException ex)
        {
            ViewBag.ErrorMessage = ex.Message;
            return View(updatePasswordDto);
        }
        catch (Exception)
        {
            return RedirectToAction("Index", "Error");
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
    public async Task<IActionResult> Logout()
    {
        await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
        return RedirectToAction("Login", "Authentication");
    }
    public IActionResult Denied()
    {
        return View();
    }
    #region Private Methods
    private async Task SetUpCookiesAsync(GetUserDto userInformationDto, bool rememberMe)
    {
        var claims = new List<Claim>
        {
            new Claim("UserId", userInformationDto.Id.ToString()),
            new Claim(ClaimTypes.Email, userInformationDto.Email),
            new Claim("Roles", userInformationDto.Roles.ToString()!),
        };

        var claimsIdentity = new ClaimsIdentity(claims, "cookie");
        var claimsPrincipal = new ClaimsPrincipal(claimsIdentity);

        await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, claimsPrincipal, new AuthenticationProperties
        {
            IsPersistent = rememberMe,
            ExpiresUtc = DateTime.UtcNow.AddMinutes(30)
        });
    }
    #endregion
}