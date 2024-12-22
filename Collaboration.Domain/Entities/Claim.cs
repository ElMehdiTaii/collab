using Collaboration.Domain.Common;

namespace Collaboration.Domain.Entities;

public class Claim : BaseEntity
{
    public string Title { get; set; } = null!;
}
