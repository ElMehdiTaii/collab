using Collaboration.Domain.DTOs.Common;
using MediatR;

namespace Collaboration.Application.Features.TaskAttachement.Commands;

public sealed record class DeleteTaskAttachementCommand(int Id) : IRequest<Response>;
