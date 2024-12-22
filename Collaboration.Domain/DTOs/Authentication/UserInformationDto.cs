namespace Collaboration.Domain.DTOs.Authentication;

public record UserInformationDto
{
    public string UserName { get; set; } = null!;
    public string Email { get; set; } = null!;
    public int[] Roles { get; set; } = [];
}
