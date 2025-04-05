using Collaboration.Application.Contracts.Persistence;
using Collaboration.Application.Exceptions;
using Collaboration.Domain.DTOs.Common;
using MediatR;

namespace Collaboration.Application.Features.Task.Commands.UpdateTaskCommand;

public sealed class UpdateTaskCommandHandler(ITaskRepository taskRepository,
    ITaskAttachementRepository taskAttachementRepository) : IRequestHandler<UpdateTaskCommand, Response>
{
    public async Task<Response> Handle(UpdateTaskCommand request, CancellationToken cancellationToken)
    {
        var response = new Response(string.Empty);

        var task = await taskRepository.GetTaskAsync(request.Id) ??
            throw new NotFoundException("Task not found", request.Id);

        task.BoardId = request.BoardId;
        task.Title = request.Title;
        task.Description = request.Description;
        task.UserId = request.AssignedTo;
        task.StartDate = request.StartDate;
        task.EndDate = request.EndDate;
        task.Priority = request.Priority;
        task.Status = request.Status;

        if (!await taskRepository.UpdateTaskAsync(task))
            throw new BadRequestException("Something was wrong");

        if (request.Attachments != null && request.Attachments.Any())
        {
            var taskAttachment = new List<Domain.Entities.TaskAttachement>();

            foreach (var attachment in request.Attachments)
            {
                taskAttachment.Add(new Domain.Entities.TaskAttachement()
                {
                    Name = attachment.Name,
                    Data = attachment.Data,
                    ContentType = attachment.ContentType,
                    TaskId = task.Id
                });
            }
            if (!await taskAttachementRepository.CreateTaskAttachementsAsync(taskAttachment))
                throw new BadRequestException("Something was wrong");
            response = new Response("Task updated successfully", task.Id);
        }
        return response;
    }
}