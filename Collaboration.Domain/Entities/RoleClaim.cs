using Collaboration.Domain.Common;

namespace Collaboration.Domain.Entities;

public class RoleClaim : BaseEntity
{
    public int RoleId { get; set; }

    public int ClaimId { get; set; }

    public virtual Claim Claim { get; set; } = null!;

    public virtual SystemRole Role { get; set; } = null!;
}
