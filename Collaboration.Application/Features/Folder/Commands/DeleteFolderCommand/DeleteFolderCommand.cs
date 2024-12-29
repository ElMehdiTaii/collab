using MediatR;

namespace Collaboration.Application.Features.Folder.Commands.DeleteFolderCommand;

public record DeleteFolderCommand : IRequest<bool>;