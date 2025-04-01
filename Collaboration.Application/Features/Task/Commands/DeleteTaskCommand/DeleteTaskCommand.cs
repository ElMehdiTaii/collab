using Collaboration.Domain.DTOs.Common;
using MediatR;

namespace Collaboration.Application.Features.Task.Commands.DeleteTaskCommand;

public sealed record DeleteTaskCommand(int Id) : IRequest<Response>;
