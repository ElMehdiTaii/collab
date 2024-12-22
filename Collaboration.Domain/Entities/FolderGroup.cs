using Collaboration.Domain.Common;

namespace Collaboration.Domain.Entities;

public class FolderGroup : BaseEntity
{
    public int FolderId { get; set; }

    public int GroupId { get; set; }

    public virtual Folder Folder { get; set; } = null!;

    public virtual Group Group { get; set; } = null!;
}
