namespace Collaboration.Domain.DTOs.Task;

public sealed record GetTaskDto(int Id,
                               string Title,
                               string Description,
                               DateTime? StartDate,
                               DateTime? EndDate,
                               int Priority,
                               int Status,
                               int UserId,
                               List<GetTaskAttachementDto>? TaskAttachements
                               );

public sealed record GetTaskAttachementDto(int Id, string Name, byte[] Data, int TaskId);
