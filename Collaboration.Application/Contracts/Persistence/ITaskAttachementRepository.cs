using Collaboration.Domain.Entities;

namespace Collaboration.Application.Contracts.Persistence;

public interface ITaskAttachementRepository : IGenericRepository<TaskAttachement>
{
    Task<bool> CreateTaskAttachementsAsync(List<TaskAttachement> taskAttachements);

    Task<bool> DeleteTaskAttachementAsync(int id);

    Task<TaskAttachement?> GetAsync(int id, CancellationToken cancellationToken = default);

}
