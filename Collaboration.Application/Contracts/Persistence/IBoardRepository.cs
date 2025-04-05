using Collaboration.Domain.Entities;

namespace Collaboration.Application.Contracts.Persistence;

public interface IBoardRepository : IGenericRepository<Board>
{
    Task<bool> CreateBoardAsync(Board board);
    Task<bool> UpdateBoardAsync(Board board);
    Task<Board?> GetBoardAsync(int id);
    Task<List<Board>> GetAllBoardAsync(int accountId, int[]? assignedTo);
}
