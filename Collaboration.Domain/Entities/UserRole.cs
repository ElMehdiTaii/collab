using Collaboration.Domain.Common;

namespace Collaboration.Domain.Entities;

public class UserRole : BaseEntity
{
    public int RoleId { get; set; }

    public int UserId { get; set; }

    public virtual SystemRole Role { get; set; } = null!;

    public virtual User User { get; set; } = null!;
}
