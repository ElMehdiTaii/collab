using MediatR;

namespace Collaboration.Application.Features.Task.Queries.GetAllTasksBoardQuery;

public record GetAllTasksBoardQuery(int BoardId) : IRequest<List<Domain.Entities.Task>>;
