using Collaboration.Domain.Common;

namespace Collaboration.Domain.Entities;

public class Document : BaseEntity
{
    public string Title { get; set; } = null!;
    public string? Description { get; set; }
    public bool IsArchived { get; set; }
    public bool IsLocked { get; set; }
    public int? VersionId { get; set; }
    public bool? IsPublic { get; set; }
    public bool? IsFavorite { get; set; }
    public int? TagId { get; set; }
    public int FolderId { get; set; }
    public DateTime? CreatedAt { get; set; }
    public DateTime? ExpiryDate { get; set; }
    public string? Path { get; set; }
    public string? Type { get; set; }
    public int? Size { get; set; }
    public virtual Folder Folder { get; set; } = null!;
    public virtual Tag? Tag { get; set; }
    public virtual Version? Version { get; set; }
    public virtual ICollection<DocumentComment> DocumentComments { get; set; } = [];
}
