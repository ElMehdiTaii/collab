using Collaboration.Application.Contracts.Persistence;
using Collaboration.Persistence.DatabaseContext;
using Microsoft.EntityFrameworkCore;

namespace Collaboration.Persistence.Repositories;
public sealed class TaskRepository(CollaborationDatabaseContext context) : GenericRepository<Domain.Entities.Task>(context), ITaskRepository
{
    public async Task<List<Domain.Entities.Task>> GetAllTaskAsync(int boardId)
    {
        return await _context.Task
                             .Include(u => u.User)
                             .Include(t => t.Board)
                             .ThenInclude(b => b.Account)
                             .Include(t => t.User)
                             .Where(t => t.BoardId == boardId)
                             .ToListAsync();
    }
    public async Task<bool> CreateTaskAsync(Domain.Entities.Task task)
    {
        _context.Task.Add(task);
        return await _context.SaveChangesAsync() > 0;
    }
    public async Task<bool> UpdateTaskAsync(Domain.Entities.Task task)
    {
        _context.Task.Update(task);
        return await _context.SaveChangesAsync() > 0;
    }
    public async Task<Domain.Entities.Task> GetTaskAsync(int id)
    {
        return await _context.Task
                             .Include(t => t.User)
                             .Include(t => t.Board)
                             .ThenInclude(b => b.Account)
                             .Include(t => t.User)
                             .FirstAsync(t => t.Id == id);
    }
    public async Task<List<Domain.Entities.Task>> GetAllTaskByAccountAsync(int accountId)
    {
        return await _context.Task
                             .Include(t => t.User)
                             .Include(t => t.Board)
                             .Where(t => t.Board.AccountId == accountId)
                             .ToListAsync();
    }
    public async Task<List<Domain.Entities.Task>> GetTasksAsync(int[]? taskId)
    {
        return await _context.Task
                             .Include(t => t.Board)
                             .ThenInclude(b => b.Account)
                             .Include(t => t.User)
                             .Where(t => taskId != null && taskId.Contains(t.Id))
                             .ToListAsync();
    }

    public async Task<bool> DeleteTaskAsync(Domain.Entities.Task task)
    {
        _context.Task.Remove(task);
        return await _context.SaveChangesAsync() > 0;
    }
}
