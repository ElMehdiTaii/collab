namespace Collaboration.Domain.DTOs.TaskAttachement;

public record TaskAttachmentDto
{
    public string Name { get; set; } = null!;
    public byte[] Data { get; set; } = null!;
    public string ContentType { get; set; } = null!;
}
