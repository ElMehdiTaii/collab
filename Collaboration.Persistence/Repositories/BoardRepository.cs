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
    public async Task<Board?> GetBoardAsync(int id)
    {
        return await _context.Board
            .Include(b => b.Tasks)
            .ThenInclude(t => t.TaskAttachements)
            .FirstOrDefaultAsync(b => b.Id == id);
    }

    public async Task<List<Board>> GetAllBoardAsync(int accountId, int[]? assignedTo)
    {
        return await _context.Board
        .Include(b => b.Tasks)
        .ThenInclude(u => u.User)
        .Where(b => b.AccountId == accountId &&
                    (assignedTo == null ||  (assignedTo != null && b.Tasks.Any(t => assignedTo.Contains(t.User.Id)))))
        .OrderByDescending(b => b.Id)
        .ToListAsync();
    }

    public async Task<bool> UpdateBoardAsync(Board board)
    {
        _context.Update(board);
        return await _context.SaveChangesAsync() > 0;
    }
}
