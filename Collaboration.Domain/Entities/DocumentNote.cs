using Collaboration.Domain.Common;

namespace Collaboration.Domain.Entities;

public class DocumentNote : BaseEntity
{
    public string Comment { get; set; } = null!;
    public DateTime? CreatedAt { get; set; }
    public int CreatedBy { get; set; }
    public int DocumentId { get; set; }
    public virtual User CreatedByNavigation { get; set; } = null!;
    public virtual Document Document { get; set; } = null!;
}
