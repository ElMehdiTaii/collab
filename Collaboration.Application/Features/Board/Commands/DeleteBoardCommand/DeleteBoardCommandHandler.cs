using Collaboration.Application.Contracts.Persistence;
using Collaboration.Application.Exceptions;
using Collaboration.Domain.DTOs.Common;
using MediatR;

namespace Collaboration.Application.Features.Board.Commands.DeleteBoardCommand;

public sealed class DeleteBoardCommandHandler(IBoardRepository boardRepository) : IRequestHandler<DeleteBoardCommand, Response>
{
    public async Task<Response> Handle(DeleteBoardCommand request, CancellationToken cancellationToken)
    {
        Domain.Entities.Board? board = await boardRepository.GetBoardAsync(request.Id)
                                       ?? throw new BadRequestException("Something was wrong");

        await boardRepository.DeleteAsync(board);

        return new Response("Board deleted successfully", board.Id);
    }
}
