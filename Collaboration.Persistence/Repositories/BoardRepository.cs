using Collaboration.Application.Contracts.Persistence;
using Collaboration.Domain.Entities;
using Collaboration.Persistence.DatabaseContext;
using Microsoft.EntityFrameworkCore;

namespace Collaboration.Persistence.Repositories;

public sealed class BoardRepository(CollaborationDatabaseContext context) : GenericRepository<Board>(context), IBoardRepository
{
    public async Task<bool> CreateBoardAsync(Board board)
    {
        _context.Add(board);
        return await _context.SaveChangesAsync() > 0;
    }

    public async Task<List<Board>> GetAllBoardAsync(int accountId)
    {
        return await _context.Board
            .Include(b => b.Tasks)
            .Where(b => b.AccountId == accountId)
            .ToListAsync();
    }
}
