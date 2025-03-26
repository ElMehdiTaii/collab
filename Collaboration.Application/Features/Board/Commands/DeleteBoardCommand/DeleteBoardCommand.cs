using Collaboration.Domain.DTOs.Common;
using MediatR;

namespace Collaboration.Application.Features.Board.Commands.DeleteBoardCommand;

public sealed record DeleteBoardCommand(int Id) : IRequest<Response>;
