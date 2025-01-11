using Collaboration.Domain.Common;

namespace Collaboration.Domain.Entities;
public class Board : BaseEntity
{
    public string Title { get; set; } = null!;
    public string? Description { get; set; }
    public bool IsPublic { get; set; }
    public bool IsArchived { get; set; }
    public bool IsLocked { get; set; }
    public bool IsFavorite { get; set; }
    public int AccountId { get; set; }
    public virtual Account Account { get; set; } = null!;
    public virtual ICollection<Task> Tasks { get; set; } = [];
}
