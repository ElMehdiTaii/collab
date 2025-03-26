using AutoMapper;
using Collaboration.Application.Exceptions;
using Collaboration.Application.Features.Folder.Commands.CreateFolderCommand;
using Collaboration.Application.Features.Folder.Commands.DeleteFolderCommand;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Collaboration.MVC.Controllers;

public class FolderController(IMapper _mapper, IMediator _mediator) : Controller
{
    public IActionResult Index()
    {
        return View();
    }

    [HttpGet]
    public async Task<IActionResult> Get()
    {
        try
        {
            return Ok(await _mediator.Send(new()));
        }
        catch (Exception ex)
        {
            return Problem(
                detail: ex.Message,
                statusCode: 500,
                title: "An unexpected error occurred."
            );
        }
    }

    [HttpPost]
    public async Task<IActionResult> Create()
    {
        try
        {
            await _mediator.Send(new CreateFolderCommand());
            return Ok();
        }
        catch (BadRequestException ex)
        {
            return BadRequest(new { message = ex.Message });
        }
        catch (Exception ex)
        {
            return Problem(
                detail: ex.Message,
                statusCode: 500,
                title: "An unexpected error occurred."
            );
        }
    }

    [HttpPost]
    public async Task<IActionResult> Update()
    {
        try
        {
            await _mediator.Send(new CreateFolderCommand());
            return Ok();
        }
        catch (BadRequestException ex)
        {
            return BadRequest(new { message = ex.Message });
        }
        catch (Exception ex)
        {
            return Problem(
                detail: ex.Message,
                statusCode: 500,
                title: "An unexpected error occurred."
            );
        }
    }

    [HttpPost]
    public async Task<IActionResult> Delete()
    {
        try
        {
            await _mediator.Send(new DeleteFolderCommand());
            return Ok();
        }
        catch (BadRequestException ex)
        {
            return BadRequest(new { message = ex.Message });
        }
        catch (Exception ex)
        {
            return Problem(
                detail: ex.Message,
                statusCode: 500,
                title: "An unexpected error occurred."
            );
        }
    }

    [HttpPost]
    public async Task<IActionResult> Bookmark()
    {
        try
        {
            await _mediator.Send(new CreateFolderCommand());
            return Ok();
        }
        catch (BadRequestException ex)
        {
            return BadRequest(new { message = ex.Message });
        }
        catch (Exception ex)
        {
            return Problem(
                detail: ex.Message,
                statusCode: 500,
                title: "An unexpected error occurred."
            );
        }
    }

    [HttpPost]
    public async Task<IActionResult> Archive()
    {
        try
        {
            await _mediator.Send(new CreateFolderCommand());
            return Ok();
        }
        catch (BadRequestException ex)
        {
            return BadRequest(new { message = ex.Message });
        }
        catch (Exception ex)
        {
            return Problem(
                detail: ex.Message,
                statusCode: 500,
                title: "An unexpected error occurred."
            );
        }
    }
}
