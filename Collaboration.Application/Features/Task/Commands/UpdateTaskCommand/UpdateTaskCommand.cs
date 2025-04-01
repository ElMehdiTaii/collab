using Collaboration.Domain.DTOs.Common;
using MediatR;

namespace Collaboration.Application.Features.Task.Commands.UpdateTaskCommand;

public record UpdateTaskCommand(int Id,
    string Title,
    string Description,
    int? Priority,
    DateTime? StartDate,
    DateTime? EndDate,
    int AssignedTo,
    int BoardId, 
    int Status) : IRequest<Response>;
