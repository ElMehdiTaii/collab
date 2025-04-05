using Collaboration.Application.Contracts.Persistence;
using Collaboration.Application.Exceptions;
using MediatR;

namespace Collaboration.Application.Features.TaskAttachement.Queries.GetTaskAttachement;

public class GetTaskAttachementQueryHandler(ITaskAttachementRepository taskAttachementRepository) : IRequestHandler<GetTaskAttachementQuery, Domain.Entities.TaskAttachement>
{
    public async Task<Domain.Entities.TaskAttachement> Handle(GetTaskAttachementQuery request, CancellationToken cancellationToken)
    {
        return await taskAttachementRepository.GetAsync(request.Id, cancellationToken) ??
            throw new NotFoundException("Attachement not found", request.Id);
    }
}
