using Collaboration.Domain.DTOs.Common;
using Collaboration.Domain.DTOs.TaskAttachement;
using MediatR;

namespace Collaboration.Application.Features.Task.Commands.UpdateTaskCommand;

public record UpdateTaskCommand : IRequest<Response>
{
    public int Id { get; set; }
    public string Title { get; set; } = null!;
    public string Description { get; set; } = null!;
    public int Priority { get; set; }
    public DateTime? StartDate { get; set; }
    public DateTime? EndDate { get; set; }
    public int AssignedTo { get; set; }
    public int BoardId { get; set; }
    public int Status { get; set; }
    public List<TaskAttachmentDto> Attachments { get; set; } = [];

}

