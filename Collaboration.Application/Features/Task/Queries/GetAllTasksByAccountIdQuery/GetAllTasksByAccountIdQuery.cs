using MediatR;

namespace Collaboration.Application.Features.Task.Queries.GetAllTasksQuery;

public record GetAllTasksByAccountIdQuery(int AccountId) : IRequest<List<Domain.Entities.Task>>;