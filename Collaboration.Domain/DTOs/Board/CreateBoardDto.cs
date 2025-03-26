namespace Collaboration.Domain.DTOs.Board;

public record CreateBoardDto(string Title, string? Description, int[]? TasksId);