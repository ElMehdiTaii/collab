using Collaboration.Application.Contracts.Persistence;
using Collaboration.Application.Exceptions;
using Collaboration.Domain.DTOs.Common;
using MediatR;

namespace Collaboration.Application.Features.Board.Commands.UpdateBoardCommand;

public class UpdateBoardCommandHandler(IBoardRepository boardRepository) : IRequestHandler<UpdateBoardCommand, Response>
{
    public async Task<Response> Handle(UpdateBoardCommand request, CancellationToken cancellationToken)
    {
        var board = await boardRepository.GetBoardAsync(request.Id)
            ?? throw new BadRequestException("Board not exist");

        board.Title = request.Title;
        board.Discription = request.Description;

        if (await boardRepository.UpdateBoardAsync(board))
            return new Response("Board updated successfully", board.Id);
        throw new BadRequestException("Something was wrong");
    }
}
