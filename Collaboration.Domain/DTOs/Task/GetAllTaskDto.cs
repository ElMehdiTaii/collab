namespace Collaboration.Domain.DTOs.Task;

public sealed record GetAllTaskDto(int Id,
                           string Title,
                           string Description,
                           string StartDate,
                           string EndDate,
                           string Priority,
                           string Status,
                           int UserId,
                           string AssignedTo);