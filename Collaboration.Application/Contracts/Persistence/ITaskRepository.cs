namespace Collaboration.Application.Contracts.Persistence;

public interface ITaskRepository : IGenericRepository<Domain.Entities.Task>
{
    Task<List<Domain.Entities.Task>> GetAllTaskAsync(int boardId);
    Task<List<Domain.Entities.Task>> GetAllTaskByAccountAsync(int accountId);
    Task<Domain.Entities.Task> GetTaskAsync(int taskId);
    Task<List<Domain.Entities.Task>> GetTasksAsync(int[]? taskId);
    Task<bool> CreateTaskAsync(Domain.Entities.Task task);
    Task<bool> DeleteTaskAsync(Domain.Entities.Task task);
    Task<bool> UpdateTaskAsync(Domain.Entities.Task task);
    Task<List<Domain.Entities.Task>> GetAllTasksByUserIdAsync(int userId);
}
