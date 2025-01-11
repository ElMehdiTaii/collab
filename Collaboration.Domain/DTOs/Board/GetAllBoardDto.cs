namespace Collaboration.Domain.DTOs.Board;

public record GetAllBoardDto(string Title,
    string Description,
    TaskStatus TaskStatus);

public record TaskStatus(int Count);

