using Collaboration.Domain.Common;

namespace Collaboration.Domain.Entities;

public class Account : BaseEntity
{
    public string Name { get; set; } = null!;

    public virtual ICollection<Board> TBoards { get; set; } = [];

    public virtual ICollection<Document> TDocuments { get; set; } = [];

    public virtual ICollection<Folder> TFolders { get; set; } = [];

    public virtual ICollection<Tag> TTags { get; set; } = [];

    public virtual ICollection<User> TUsers { get; set; } = [];

    public virtual ICollection<Version> TVersions { get; set; } = [];
}