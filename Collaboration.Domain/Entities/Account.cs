using Collaboration.Domain.Common;

namespace Collaboration.Domain.Entities;

public class Account : BaseEntity
{
    public string Name { get; set; } = null!;
    public virtual ICollection<Board> TBoards { get; set; } = new List<Board>();
    public virtual ICollection<Document> TDocuments { get; set; } = new List<Document>();
    public virtual ICollection<Folder> TFolders { get; set; } = new List<Folder>();
    public virtual ICollection<SystemRole> TSystemRoles { get; set; } = new List<SystemRole>();
    public virtual ICollection<Tag> TTags { get; set; } = new List<Tag>();
    public virtual ICollection<User> TUsers { get; set; } = new List<User>();
    public virtual ICollection<Version> TVersions { get; set; } = new List<Version>();
}