using AutoMapper;
using Collaboration.Application.Exceptions;
using Collaboration.Application.Features.Board.Commands.CreateBoardCommand;
using Collaboration.Application.Features.Board.Commands.DeleteBoardCommand;
using Collaboration.Application.Features.Board.Commands.UpdateBoardCommand;
using Collaboration.Application.Features.Board.Queries.GetAllBoardQuery;
using Collaboration.Domain.DTOs.Board;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Collaboration.MVC.Controllers;

public class BoardController(IMapper _mapper, IMediator _mediator) : Controller
{
    public IActionResult Index()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Get([FromBody]int[] assignedTo)
    {
        try
        {
            var query = await _mediator.Send(new GetAllBoardQuery(assignedTo));
            return Ok(_mapper.Map<List<GetBoardDto>>(query.ToList()));
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
    public async Task<IActionResult> Create([FromBody] CreateBoardDto createBoardDto)
    {
        try
        {
            var command = _mapper.Map<CreateBoardCommand>(createBoardDto);
            var result = await _mediator.Send(command);
            return Ok(new { message = result.Message });
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
    public async Task<IActionResult> Update([FromBody] UpdateBoardDto updateBoardDto)
    {
        try
        {
            var command = _mapper.Map<UpdateBoardCommand>(updateBoardDto);
            var result = await _mediator.Send(command);
            return Ok(new { message = result.Message });
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
            var result = await _mediator.Send(new DeleteBoardCommand(id));
            return Ok(new { message = result.Message });
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
