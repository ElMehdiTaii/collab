using MediatR;

namespace Collaboration.Application.Features.Folder.Commands.CreateFolderCommand;

public record CreateFolderCommand() : IRequest<bool>;