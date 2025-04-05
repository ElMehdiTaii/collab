using Collaboration.Application.Contracts.Persistence;
using MediatR;

namespace Collaboration.Application.Features.Task.Queries.GetAllTasksUserQuery;

public sealed class GetAllTasksUserQueryHandler(ITaskRepository taskRepository) : IRequestHandler<GetAllTasksUserQuery, List<Domain.Entities.Task>>
{
    public Task<List<Domain.Entities.Task>> Handle(GetAllTasksUserQuery request, CancellationToken cancellationToken)
    {
        return taskRepository.GetAllTasksByUserIdAsync(request.UserId);
    }
}
