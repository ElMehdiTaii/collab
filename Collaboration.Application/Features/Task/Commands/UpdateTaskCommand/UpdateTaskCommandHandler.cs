using Collaboration.Application.Contracts.Persistence;
using Collaboration.Application.Exceptions;
using Collaboration.Domain.DTOs.Common;
using MediatR;

namespace Collaboration.Application.Features.Task.Commands.UpdateTaskCommand;

public sealed class UpdateTaskCommandHandler(ITaskRepository taskRepository) : IRequestHandler<UpdateTaskCommand, Response>
{
    public async Task<Response> Handle(UpdateTaskCommand request, CancellationToken cancellationToken)
    {
        var task = await taskRepository.GetTaskAsync(request.Id);

        task.BoardId = request.BoardId;
        task.Title = request.Title;
        task.Description = request.Description;
        task.UserId = request.AssignedTo;
        task.StartDate = request.StartDate;
        task.EndDate = request.EndDate;
        task.Priority = request.Priority;
        task.Status = request.Status;

        if (await taskRepository.CreateTaskAsync(task))
            return new Response("Task created successfully", task.Id);
        throw new BadRequestException("Something was wrong");
    }
}