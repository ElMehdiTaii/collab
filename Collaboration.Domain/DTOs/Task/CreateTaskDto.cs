namespace Collaboration.Domain.DTOs.Task;

public record CreateTaskDto(
    string Title,
    string Description,
    int? Priority,
    DateTime? StartDate,
    DateTime? EndDate,
    int AssignedTo,
    int BoardId);

