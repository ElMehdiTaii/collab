using Collaboration.Domain.Common;

namespace Collaboration.Domain.Entities;

public class Account : BaseEntity
{
    public string Name { get; set; } = null!;
    public virtual ICollection<Board> Boards { get; set; } = new List<Board>();
    public virtual ICollection<Document> Documents { get; set; } = new List<Document>();
    public virtual ICollection<Folder> Folders { get; set; } = new List<Folder>();
    public virtual ICollection<SystemRole> SystemRoles { get; set; } = new List<SystemRole>();
    public virtual ICollection<Tag> Tags { get; set; } = new List<Tag>();
    public virtual ICollection<User> Users { get; set; } = new List<User>();
    public virtual ICollection<Version> Versions { get; set; } = new List<Version>();
}