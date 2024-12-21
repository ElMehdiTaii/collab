using Collaboration.Domain.Common;

namespace Collaboration.Domain.Entities;

public class Version : BaseEntity
{
    public string? Name { get; set; }
    public int AccountId { get; set; }
    public virtual Account Account { get; set; } = null!;
    public virtual ICollection<Document> DocumentVersions { get; set; } = [];
    public virtual ICollection<Folder> FolderVersions { get; set; } = [];
}
