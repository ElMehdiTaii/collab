using Collaboration.Domain.Common;

namespace Collaboration.Domain.Entities;

public class TaskAttachement : BaseEntity
{
    public string Name { get; set; } = null!;

    public byte[] Data { get; set; } = null!;

    public string? ContentType { get; set; }

    public int TaskId { get; set; }

    public virtual Task Task { get; set; } = null!;
}
