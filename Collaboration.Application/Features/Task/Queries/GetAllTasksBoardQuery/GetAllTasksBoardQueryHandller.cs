using Collaboration.Application.Contracts.Persistence;
using MediatR;

namespace Collaboration.Application.Features.Task.Queries.GetAllTasksBoardQuery;

public sealed class GetAllTasksBoardQueryHandller(ITaskRepository taskRepository) : IRequestHandler<GetAllTasksBoardQuery, List<Domain.Entities.Task>>
{
    public async Task<List<Domain.Entities.Task>> Handle(GetAllTasksBoardQuery request, CancellationToken cancellationToken)
    {
        return await taskRepository.GetAllTaskAsync(request.BoardId);
    }
}
