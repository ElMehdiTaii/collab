namespace Collaboration.Domain.DTOs.Task;

public record UpdateTaskDto(
    int Id,
    string Title,
    string Description,
    int? Priority,
    DateTime? StartDate,
    DateTime? EndDate,
    int AssignedTo,
    int BoardId,
    int Status);
