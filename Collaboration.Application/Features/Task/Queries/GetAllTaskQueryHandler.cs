using MediatR;

namespace Collaboration.Application.Features.Task.Queries;

public sealed class GetAllTaskQueryHandler : IRequestHandler<GetAllTaskQuery, List<Domain.Entities.Task>>
{
    public Task<List<Domain.Entities.Task>> Handle(GetAllTaskQuery request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
