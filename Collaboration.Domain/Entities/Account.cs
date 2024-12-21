using Collaboration.Domain.Common;

namespace Collaboration.Domain.Entities;

public class Account : BaseEntity
{
    public string? Name { get; set; }
    public virtual ICollection<Folder> Folders { get; set; } = [];
    public virtual ICollection<Tag> Tags { get; set; } = [];
    public virtual ICollection<Version> Versions { get; set; } = [];
    public virtual ICollection<User> Users { get; set; } = [];
}