using Collaboration.Application.Contracts.Persistence;
using Collaboration.Application.Exceptions;
using Collaboration.Domain.DTOs.Common;
using MediatR;

namespace Collaboration.Application.Features.Task.Commands.CreateTaskCommand;

public class CreateTaskCommandHandler(ITaskRepository taskRepository) 
    : IRequestHandler<CreateTaskCommand, Response>
{
    public async Task<Response> Handle(CreateTaskCommand request, CancellationToken cancellationToken)
    {
        var task = new Domain.Entities.Task()
        {
            BoardId = request.BoardId,
            Title = request.Title,
            Description = request.Description,
            UserId = request.AssignedTo,
            StartDate = request.StartDate,
            EndDate = request.EndDate,
            Priority = request.Priority,
            Status = 1
        };

        if (await taskRepository.CreateTaskAsync(task))
            return new Response("Task created successfully", task.Id);
        throw new BadRequestException("Something was wrong");
    }
}
