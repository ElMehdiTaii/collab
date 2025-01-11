using Collaboration.Domain.DTOs.Common;
using MediatR;

namespace Collaboration.Application.Features.Board.Commands.CreateBoardCommand;

public record CreateBoardCommand(string Title,string Description)
    : IRequest<Response>;