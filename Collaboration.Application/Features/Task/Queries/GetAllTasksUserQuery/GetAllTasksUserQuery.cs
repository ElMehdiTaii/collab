using MediatR;

namespace Collaboration.Application.Features.Task.Queries.GetAllTasksUserQuery;

public record GetAllTasksUserQuery(int UserId) : IRequest<List<Domain.Entities.Task>>;
