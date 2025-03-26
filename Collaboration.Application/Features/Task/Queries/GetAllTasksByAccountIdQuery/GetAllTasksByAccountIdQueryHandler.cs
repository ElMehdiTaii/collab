using Collaboration.Application.Contracts.Persistence;
using MediatR;

namespace Collaboration.Application.Features.Task.Queries.GetAllTasksQuery;

public class GetAllTasksByAccountIdQueryHandler(ITaskRepository taskRepository) : IRequestHandler<GetAllTasksByAccountIdQuery, List<Domain.Entities.Task>>
{
    public async Task<List<Domain.Entities.Task>> Handle(GetAllTasksByAccountIdQuery request, CancellationToken cancellationToken)
    {
        return await taskRepository.GetAllTaskByAccountAsync(request.AccountId);
    }
}
