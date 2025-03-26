using MediatR;

namespace Collaboration.Application.Features.Board.Queries.GetAllBoardQuery;

public record GetAllBoardQuery(int[]? AssignedTo) : IRequest<List<Domain.Entities.Board>>;
