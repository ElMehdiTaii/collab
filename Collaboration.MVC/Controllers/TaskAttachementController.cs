using Collaboration.Application.Exceptions;
using Collaboration.Application.Features.TaskAttachement.Commands;
using Collaboration.Application.Features.TaskAttachement.Queries.GetTaskAttachement;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Collaboration.MVC.Controllers;

public class TaskAttachementController(IMediator _mediator) : Controller
{
    public IActionResult Get(int id)
    {
        return View();
    }
    public IActionResult GetAll(int taskId)
    {
        return View();
    }
    [HttpDelete]
    public async Task<IActionResult> Delete(int id)
    {
        try
        {
            return Ok((await _mediator.Send(new DeleteTaskAttachementCommand(id))).Message);
        }
        catch (BadRequestException ex)
        {
            return BadRequest(new
            {
                message = ex.Message
            });
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
    public async Task<IActionResult> Download(int id)
    {
        try
        {
            var attachment = await _mediator.Send(new GetTaskAttachementQuery(id));
            return File(attachment.Data, "application/octet-stream", attachment.Name);
        }
        catch (BadRequestException ex)
        {
            return BadRequest(new
            {
                message = ex.Message
            });
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
