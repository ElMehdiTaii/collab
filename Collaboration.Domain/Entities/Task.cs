using Collaboration.Domain.Common;

namespace Collaboration.Domain.Entities;

public class Task : BaseEntity
{
    public string Title { get; set; } = null!;
    public string Description { get; set; } = null!;
    public int? Status { get; set; }
    public int? Priority { get; set; }
    public DateTime? StartDate { get; set; }
    public DateTime? EndDate { get; set; }
    public int UserId { get; set; }
    public int BoardId { get; set; }
    public virtual Board Board { get; set; } = null!;
    public virtual ICollection<TaskAttachement> TaskAttachements { get; set; } = [];
    public virtual User User { get; set; } = null!;
}