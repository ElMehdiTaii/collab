namespace Collaboration.Domain.DTOs.Board;

public record GetBoardDto(
    int Id,
    string Title,
    string Discription,
    List<BoardTeamDto> BoardTeam,
    int TaskProgress,
    string Status,
    int TaskCount,
    int CompletedTask
)
{
    public GetBoardDto() : this(0, string.Empty, string.Empty, new List<BoardTeamDto>(), 0, string.Empty, 0, 0) { }
}
public record BoardTeamDto(string FullName);
