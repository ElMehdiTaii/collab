using Collaboration.Application.Contracts.Persistence;
using Collaboration.Application.Exceptions;
using Collaboration.Domain.DTOs.Common;
using Collaboration.Domain.Entities;
using MediatR;

namespace Collaboration.Application.Features.Task.Commands.CreateTaskCommand;

public class CreateTaskCommandHandler(ITaskRepository taskRepository, ITaskAttachementRepository taskAttachementRepository)
    : IRequestHandler<CreateTaskCommand, Response>
{
    public async Task<Response> Handle(CreateTaskCommand request, CancellationToken cancellationToken)
    {
        var response = new Response(string.Empty);

        var task = new Domain.Entities.Task()
        {
            BoardId = request.BoardId,
            Title = request.Title,
            Description = request.Description,
            UserId = request.AssignedTo,
            StartDate = request.StartDate,
            EndDate = request.EndDate,
            Priority = request.Priority,
            Status = (int)Domain.Enums.TaskStatus.NEW
        };

        if (!await taskRepository.CreateTaskAsync(task))
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
            response = new Response("Task created successfully", task.Id);
        }
        return response;
    }
}
