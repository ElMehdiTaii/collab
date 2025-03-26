namespace Collaboration.Domain.DTOs.Task;

public sealed record GetTaskDto(int Id,
                               string Title,
                               string Description,
                               DateTime? StartDate,
                               DateTime? EndDate,
                               string Priority,
                               string Status,
                               int UserId);
