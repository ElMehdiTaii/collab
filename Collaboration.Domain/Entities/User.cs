using Collaboration.Domain.Common;
using System.Security.Principal;

namespace Collaboration.Domain.Entities;

public class User : BaseEntity
{
    public string FullName { get; set; } = null!;

    public string Email { get; set; } = null!;

    public byte[] PasswordHash { get; set; } = null!;

    public byte[] PasswordSalt { get; set; } = null!;

    public bool IsLocked { get; set; }

    public bool IsDeleted { get; set; }

    public int AccountId { get; set; }

    public virtual Account Account { get; set; } = null!;

    public virtual ICollection<FolderNote> TFolderNotes { get; set; } = [];
}
