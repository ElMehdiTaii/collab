using Collaboration.Application.Contracts.Persistence;
using Collaboration.Domain.Entities;
using Collaboration.Persistence.DatabaseContext;
using Microsoft.EntityFrameworkCore;

namespace Collaboration.Persistence.Repositories;

public sealed class TaskAttachementRepository(CollaborationDatabaseContext context) : GenericRepository<TaskAttachement>(context), ITaskAttachementRepository
{
    public async Task<bool> CreateTaskAttachementsAsync(List<TaskAttachement> taskAttachements)
    {
        context.TaskAttachement.AddRange(taskAttachements);
        return await context.SaveChangesAsync() > 0;
    }
    public async Task<bool> DeleteTaskAttachementAsync(int id)
    {
        var taskAttachement = await context.TaskAttachement
            .FirstAsync(ta => ta.Id == id);
        context.TaskAttachement.Remove(taskAttachement!);
        return await context.SaveChangesAsync() > 0;
    }

    public async Task<TaskAttachement?> GetAsync(int id, CancellationToken cancellationToken = default)
    {
        return await context.TaskAttachement.FirstOrDefaultAsync(ta => ta.Id == id, cancellationToken);
    }
}
