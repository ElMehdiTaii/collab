namespace Collaboration.Domain.DTOs.Task;

public record GetTaskKpiDto(int All, int InProgress, int Completed, int Closed);