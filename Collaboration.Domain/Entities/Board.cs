namespace Collaboration.Domain.Entities;

public class Board
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public bool Private { get; set; }
}
