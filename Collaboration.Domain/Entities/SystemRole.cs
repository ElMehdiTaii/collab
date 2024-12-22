using Collaboration.Domain.Common;
using System.Security.Principal;

namespace Collaboration.Domain.Entities;

public class SystemRole : BaseEntity
{
    public string Title { get; set; } = null!;

    public int AccountId { get; set; }

    public virtual Account Account { get; set; } = null!;

    public virtual ICollection<RoleClaim> RoleClaims { get; set; } = [];

    public virtual ICollection<UserRole> TUserRoles { get; set; } = [];
}
