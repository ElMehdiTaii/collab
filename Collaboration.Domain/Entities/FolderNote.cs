using Collaboration.Domain.Common;

namespace Collaboration.Domain.Entities;

public class FolderNote : BaseEntity
{
    public string Note { get; set; } = null!;

    public DateTime CreatedAt { get; set; }

    public int CreatedBy { get; set; }

    public int FolderId { get; set; }

    public virtual User CreatedByNavigation { get; set; } = null!;

    public virtual Folder Folder { get; set; } = null!;
}
