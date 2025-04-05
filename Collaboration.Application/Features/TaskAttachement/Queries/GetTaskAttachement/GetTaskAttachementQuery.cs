using MediatR;

namespace Collaboration.Application.Features.TaskAttachement.Queries.GetTaskAttachement;

public record GetTaskAttachementQuery(int Id) : IRequest<Domain.Entities.TaskAttachement>;
