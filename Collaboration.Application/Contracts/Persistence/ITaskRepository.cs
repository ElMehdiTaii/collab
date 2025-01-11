namespace Collaboration.Application.Contracts.Persistence;

public interface ITaskRepository : IGenericRepository<Domain.Entities.Task>
{
    Task<List<Domain.Entities.Task>> GetAllTaskAsync(int boardId);

    Task<Domain.Entities.Task> GetTaskAsync(int taskId);

    Task<bool> CreateTaskAsync(Domain.Entities.Task task);
}
