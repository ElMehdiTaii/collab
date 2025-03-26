namespace Collaboration.Domain.DTOs.User;

public sealed record GetUserDto 
{
    public int Id { get; set; }
    public string FullName { get; set; } = null!;
    public string Email { get; set; } = null!;
    public int[] Roles { get; set; } = [];
}
