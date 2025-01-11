using AutoMapper;
using Collaboration.Application.Exceptions;
using Collaboration.Application.Features.Board.Commands.CreateBoardCommand;
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

    [HttpGet]
    public async Task<IActionResult> Get()
    {
        try
        {
            return Ok(await _mediator.Send(new GetAllBoardQuery()));
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
    public async Task<IActionResult> Create(CreateBoardDto createBoardDto)
    {
        try
        {
            var command = _mapper.Map<CreateBoardCommand>(createBoardDto);
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
