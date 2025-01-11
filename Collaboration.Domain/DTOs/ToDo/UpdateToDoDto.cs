namespace Collaboration.Domain.DTOs.ToDo;

public record UpdateToDoDto(int Id, string Title, string Status, int UserId);
