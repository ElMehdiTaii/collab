using AutoMapper;
using Collaboration.Application.Features.Task.Commands.CreateTaskCommand;
using Collaboration.Application.Features.Task.Commands.UpdateTaskCommand;
using Collaboration.Application.Features.Task.Queries;
using Collaboration.Domain.DTOs.Task;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using SendGrid.Helpers.Errors.Model;

namespace Collaboration.MVC.Controllers;

public class TaskController(IMapper _mapper, IMediator _mediator) : Controller
{
    public IActionResult Index()
    {
        return View();
    }

    [HttpGet]
    public async Task<IActionResult> Get(int boardId)
    {
        try
        {
            return Ok(await _mediator.Send(new GetAllTaskQuery(boardId)));
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
    public async Task<IActionResult> Create(CreateTaskDto createTaskDto)
    {
        try
        {
            var command = _mapper.Map<CreateTaskCommand>(createTaskDto);
            var result = await _mediator.Send(command);
            return Ok(result.Message);
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
    public async Task<IActionResult> Update(UpdateTaskDto updateTaskDto)
    {
        try
        {
            var command = _mapper.Map<UpdateTaskCommand>(updateTaskDto);
            var result = await _mediator.Send(command);
            return Ok(result.Message);
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
