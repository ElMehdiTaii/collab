using Collaboration.Domain.Entities;

namespace Collaboration.Application.Contracts.Persistence;

public interface IBoardRepository : IGenericRepository<Board>
{
    Task<bool> CreateBoardAsync(Board board);
    Task<List<Board>> GetAllBoardAsync(int accountId);
}
