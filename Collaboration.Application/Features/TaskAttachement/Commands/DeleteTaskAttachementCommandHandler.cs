using Collaboration.Application.Contracts.Persistence;
using Collaboration.Application.Exceptions;
using Collaboration.Domain.DTOs.Common;
using MediatR;

namespace Collaboration.Application.Features.TaskAttachement.Commands;

public sealed class DeleteTaskAttachementCommandHandler(ITaskAttachementRepository taskAttachementRepository) : IRequestHandler<DeleteTaskAttachementCommand, Response>
{
    public async Task<Response> Handle(DeleteTaskAttachementCommand request, CancellationToken cancellationToken)
    {
        var task = await taskAttachementRepository.DeleteTaskAttachementAsync(request.Id);

        if (task)
            return new Response("Atatchement deleted successfully");
        throw new BadRequestException("Something was wrong");
    }
}