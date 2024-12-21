using Collaboration.Domain.Common;

namespace Collaboration.Domain.Entities;

public class Folder : BaseEntity
{
    public string Title { get; set; } = null!;
    public string? Description { get; set; }
    public bool IsArchived { get; set; }
    public bool IsLocked { get; set; }
    public int? VersionId { get; set; }
    public int? TagId { get; set; }
    public int? FolderMainId { get; set; }
    public DateTime CreatedAt { get; set; }
    public bool IsPublic { get; set; }
    public bool IsFavorite { get; set; }
    public int AccountId { get; set; }
    public virtual Account? Account { get; set; } = null;
    public virtual Folder? FolderMain { get; set; }
    public virtual ICollection<Folder> InverseFolderMain { get; set; } = [];
    public virtual ICollection<Document> Documents { get; set; } = [];
    public virtual ICollection<FolderComment> FolderComments { get; set; } = [];
    public virtual Tag? Tag { get; set; }
    public virtual Version? Version { get; set; }
}
