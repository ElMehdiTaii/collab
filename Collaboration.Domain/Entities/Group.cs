using Collaboration.Domain.Common;

namespace Collaboration.Domain.Entities;
public class Group : BaseEntity
{
    public string Title { get; set; } = null!;

    public virtual ICollection<FolderGroup> TFolderGroups { get; set; } = [];

    public virtual ICollection<UserGroup> TUserGroups { get; set; } = [];
}
