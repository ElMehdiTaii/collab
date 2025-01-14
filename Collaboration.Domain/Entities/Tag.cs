using Collaboration.Domain.Common;

namespace Collaboration.Domain.Entities;

public class Tag : BaseEntity
{
    public string Name { get; set; } = null!;

    public int AccountId { get; set; }

    public virtual Account Account { get; set; } = null!;

    public virtual ICollection<Document> Documents { get; set; } = [];

    public virtual ICollection<Folder> Folders { get; set; } = [];
}
