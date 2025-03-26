using AutoMapper;
using Collaboration.Application.Exceptions;
using Collaboration.Application.Features.ToDo.Commands.DeleteToDoCammand;
using Collaboration.Application.Features.ToDo.Commands.UpdateToDoCammand;
using Collaboration.Application.Features.ToDo.Queries.GetAllToDoQuerie;
using Collaboration.Domain.DTOs.ToDo;
using Collaboration.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Collaboration.MVC.Controllers;

public class ToDoController(IMapper _mapper, IMediator _mediator) : Controller
{
    [HttpGet]
    public async Task<IActionResult> Get()
    {
        try
        {
            return Ok(await _mediator.Send(new GetAllToDoQuerie()));
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

    [HttpGet]
    public async Task<IActionResult> Update(UpdateToDoDto updateToDoDto)
    {
        try
        {
            var command = _mapper.Map<ToDo>(updateToDoDto);
            var result = await _mediator.Send(new UpdateToDoCammand(command));
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
    public async Task<IActionResult> Delete(int id)
    {
        try
        {
            var result = await _mediator.Send(new DeleteToDoCammandHandler(id));
            return Ok(result);
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
