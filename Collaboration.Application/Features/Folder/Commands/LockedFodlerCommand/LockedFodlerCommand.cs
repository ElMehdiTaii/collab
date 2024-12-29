using MediatR;

namespace Collaboration.Application.Features.Folder.Commands.LockedFodlerCommand;

public record LockedFodlerCommand : IRequest<bool>;
