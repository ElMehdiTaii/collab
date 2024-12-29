using MediatR;

namespace Collaboration.Application.Features.Folder.Commands.UpdateFolderCommand;

public record UpdateFolderCommand : IRequest<bool>;
