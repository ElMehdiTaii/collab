using Collaboration.Domain.Common;

namespace Collaboration.Domain.Entities;

public class Tag : BaseEntity
{
    public string? Name { get; set; }

    public int? AccountId { get; set; }

    public virtual Account Account { get; set; } = null!;

    public virtual ICollection<Document> DocumentTags { get; set; } = [];

    public virtual ICollection<Folder> TFolderTags { get; set; } = [];
}
