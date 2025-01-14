using Collaboration.Application.Contracts.Persistence;
using Collaboration.Application.Exceptions;
using Collaboration.Domain.DTOs.Common;
using MediatR;

namespace Collaboration.Application.Features.Board.Commands.CreateBoardCommand;

public class CreateBoardCommandHandler(IBoardRepository boardRepository) : IRequestHandler<CreateBoardCommand, Response>
{
    public async Task<Response> Handle(CreateBoardCommand request, CancellationToken cancellationToken)
    {
        Domain.Entities.Board board = new()
        {
            Title = request.Title,
            Discription = request.Description,
            AccountId = 1
        };

        if (await boardRepository.CreateBoardAsync(board))
            return new Response("Board created successfully", board.Id);
        throw new BadRequestException("Something was wrong");
    }
}
