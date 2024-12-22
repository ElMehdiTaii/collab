using Collaboration.Domain.Common;

namespace Collaboration.Domain.Entities;

public class UserGroup : BaseEntity
{
    public int UserId { get; set; }
    public int GroupId { get; set; }

    public virtual Group Group { get; set; } = null!;

    public virtual User User { get; set; } = null!;
}
