using MediatR;

namespace Collaboration.Application.Features.Board.Queries.GetAllBoardQuery;

public record GetAllBoardQuery : IRequest<List<Domain.Entities.Board>>;
