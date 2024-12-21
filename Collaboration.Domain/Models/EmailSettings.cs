namespace Collaboration.Domain.Models;

public record EmailSettings
{
    public string ApiKey { get; set; } = null!;
    public string FromAddress { get; set; } = null!;
    public string FromName { get; set; } = null!;
}
