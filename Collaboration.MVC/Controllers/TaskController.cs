using AutoMapper;
using Collaboration.Application.Features.Task.Commands.CreateTaskCommand;
using Collaboration.Application.Features.Task.Commands.UpdateTaskCommand;
using Collaboration.Application.Features.Task.Queries.GetAllTasksBoardQuery;
using Collaboration.Application.Features.Task.Queries.GetAllTasksQuery;
using Collaboration.Application.Features.Task.Queries.GetTaskByIdQeury;
using Collaboration.Domain.DTOs.Task;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using SendGrid.Helpers.Errors.Model;

namespace Collaboration.MVC.Controllers;

public class TaskController(IMapper _mapper, IMediator _mediator) : Controller
{
    public IActionResult Index(int id)
    {
        ViewBag.BoardId = id;
        return View();
    }

    [HttpGet]
    public async Task<IActionResult> Get(int id)
    {
        try
        {
            var query = await _mediator.Send(new GetTaskByIdQuery(id));
            return Ok(_mapper.Map<GetTaskDto>(query));
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
    public async Task<IActionResult> GetAll(int boardId)
    {
        try
        {
            var query = await _mediator.Send(new GetAllTasksBoardQuery(boardId));
            return Ok(_mapper.Map<List<GetTaskDto>>(query.ToList()));
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
    public async Task<IActionResult> Create([FromBody] CreateTaskDto createTaskDto)
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

    [HttpGet]
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

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        try
        {
            int accountId = 1;
            var query = await _mediator.Send(new GetAllTasksByAccountIdQuery(accountId));
            return Ok(_mapper.Map<List<GetTaskDto>>(query.ToList()));
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
