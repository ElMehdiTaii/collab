using Collaboration.Domain.DTOs.Common;
using MediatR;

namespace Collaboration.Application.Features.Task.Commands.CreateTaskCommand;

public record CreateTaskCommand(string Title, 
    string Description,
    int? Priority,
    DateTime? StartDate,
    DateTime? EndDate,
    int AssignedTo,
    int BoardId
    public int Status { get; set; }
