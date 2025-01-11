using Collaboration.Application.Contracts.Persistence;
using Collaboration.Persistence.DatabaseContext;
using Microsoft.EntityFrameworkCore;

namespace Collaboration.Persistence.Repositories;
public sealed class TaskRepository(CollaborationDatabaseContext context) : GenericRepository<Domain.Entities.Task>(context), ITaskRepository
{
    public async Task<List<Domain.Entities.Task>> GetAllTaskAsync(int boardId)
    {
        return await _context.Task
            .Include(t => t.Board)
            .ThenInclude(b => b.Account)
            .Include(t => t.User)
            .Where(t => t.BoardId == boardId).ToListAsync();
    }

    public async Task<bool> CreateTaskAsync(Domain.Entities.Task task)
    {
        _context.Task.Add(task);
        return await _context.SaveChangesAsync() > 0;
    }

    public async System.Threading.Tasks.Task<Domain.Entities.Task> GetTaskAsync(int id)
    {
        return await _context.Task
             .Include(t => t.Board)
             .ThenInclude(b => b.Account)
             .Include(t => t.User)
             .FirstAsync(t => t.Id == id);
    }
}
