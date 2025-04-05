using Collaboration.Domain.DTOs.Common;
using MediatR;

namespace Collaboration.Application.Features.Board.Commands.UpdateBoardCommand;

public record UpdateBoardCommand(int Id, string Title, string Description) : IRequest<Response>;
