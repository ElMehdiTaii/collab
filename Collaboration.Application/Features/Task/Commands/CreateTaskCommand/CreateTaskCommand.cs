using Collaboration.Domain.DTOs.Common;
using Collaboration.Domain.DTOs.TaskAttachement;
using MediatR;

namespace Collaboration.Application.Features.Task.Commands.CreateTaskCommand;

public record CreateTaskCommand : IRequest<Response>
{
    public string Title { get; set; } = null!;
    public string Description { get; set; } = null!;
    public DateTime? StartDate { get; set; } = null!;
    public DateTime? EndDate { get; set; } = null!;
    public int Priority { get; set; }
    public int Status { get; set; }
    public int AssignedTo { get; set; }
    public int BoardId { get; set; }
    public List<TaskAttachmentDto> Attachments { get; set; } = [];
}
