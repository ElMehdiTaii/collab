using Collaboration.Domain.Common;

namespace Collaboration.Domain.Entities;

public class User : BaseEntity
{
    public string FullName { get; set; } = null!;
    public string? Email { get; set; } = null!;
    public byte[] PasswordHash { get; set; } = null!;
    public byte[] PasswordSalt { get; set; } = null!;
    public bool IsLockedAccount { get; set; }
    public int AccountId { get; set; }
    public virtual Account? Account { get; set; }
}
