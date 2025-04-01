using AutoMapper;
using Collaboration.Application.Features.Task.Commands.CreateTaskCommand;
using Collaboration.Application.Features.Task.Commands.DeleteTaskCommand;
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

    [HttpGet]
    public async Task<IActionResult> TaskKpis(int boardId)
    {
        try
        {
            var query = await _mediator.Send(new GetAllTasksBoardQuery(boardId));
            var kpiDto = new GetTaskKpiDto(
            All: query.Count(),
            InProgress: query.Count(t => t.Status == (int)Domain.Enums.TaskStatus.IN_PROGRESS),
            Completed: query.Count(t => t.Status == (int)Domain.Enums.TaskStatus.COMPLETED),
            Closed: query.Count(t => t.Status == (int)Domain.Enums.TaskStatus.CLOSED));
            return Ok(kpiDto);
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
    public async Task<IActionResult> GetAll(int boardId)
    {
        try
        {
            var query = await _mediator.Send(new GetAllTasksBoardQuery(boardId));
            return Ok(_mapper.Map<List<GetAllTaskDto>>(query.ToList()));
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

    [HttpPut]
    public async Task<IActionResult> Update(int id, [FromBody] UpdateTaskDto updateTaskDto)
    {
        try
        {
            var command = _mapper.Map<UpdateTaskCommand>(updateTaskDto with { Id = id });
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
    public async Task<IActionResult> GetAllReferences()
    {
        try
        {
            int accountId = 1;
            var query = await _mediator.Send(new GetAllTasksByAccountIdQuery(accountId));
            return Ok(_mapper.Map<List<GetAllTaskDto>>(query.ToList()));
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

    [HttpDelete]
    public async Task<IActionResult> Delete(int id)
    {
        try
        {
            var command = await _mediator.Send(new DeleteTaskCommand(id));
            return Ok(command.Message);
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
