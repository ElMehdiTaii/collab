using Collaboration.Application.Contracts.Persistence;
using MediatR;

namespace Collaboration.Application.Features.Task.Queries.GetTaskByIdQeury;

public class GetTaskByIdQueryHandler(ITaskRepository taskRepository) : IRequestHandler<GetTaskByIdQuery, Domain.Entities.Task>
{
    public async Task<Domain.Entities.Task> Handle(GetTaskByIdQuery request, CancellationToken cancellationToken)
    {
        return await taskRepository.GetByIdAsync(request.Id);
    }
}