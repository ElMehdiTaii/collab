using Collaboration.Application.Contracts.Persistence;
using Collaboration.Application.Exceptions;
using MediatR;

namespace Collaboration.Application.Features.Board.Queries.GetAllBoardQuery;

public sealed class GetAllBoardQueryHandler(IBoardRepository boardRepository) : IRequestHandler<GetAllBoardQuery, List<Domain.Entities.Board>>
{
    public Task<List<Domain.Entities.Board>> Handle(GetAllBoardQuery request, CancellationToken cancellationToken)
    {
        try
        {
            int accountId = 1;
            return boardRepository.GetAllBoardAsync(accountId, request.AssignedTo);
        }
        catch
        {
            throw new BadRequestException("Something was wrong");
        }
    }
}

