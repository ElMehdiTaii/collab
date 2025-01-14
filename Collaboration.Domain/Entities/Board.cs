using Collaboration.Domain.Common;
using System.Security.Principal;

namespace Collaboration.Domain.Entities;
public class Board : BaseEntity
{
    public string Title { get; set; } = null!;

    public string? Discription { get; set; }

    public bool IsPublic { get; set; } = false;

    public bool IsArchived { get; set; } = false;

    public bool IsLocked { get; set; } = false;

    public bool IsFavorite { get; set; } = false;

    public int AccountId { get; set; }

    public virtual Account Account { get; set; } = null!;

    public virtual ICollection<Task> Tasks { get; set; } = [];

}
