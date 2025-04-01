using Collaboration.Application.Contracts.Persistence;
using Collaboration.Application.Exceptions;
using Collaboration.Domain.DTOs.Common;
using MediatR;

namespace Collaboration.Application.Features.Task.Commands.DeleteTaskCommand;

public sealed class DeleteTaskCommandHandler(ITaskRepository taskRepository) : IRequestHandler<DeleteTaskCommand, Response>
{
    public async Task<Response> Handle(DeleteTaskCommand request, CancellationToken cancellationToken)
    {
        var task = await taskRepository.GetTaskAsync(request.Id) ??
            throw new NotFoundException("Task not found", request.Id);
            

        if (await taskRepository.DeleteTaskAsync(task))
            return new Response("Task deleted successfully", task.Id);
        throw new BadRequestException("Something was wrong");
    }
}
