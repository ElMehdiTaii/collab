using MediatR;

namespace Collaboration.Application.Features.Task.Queries;

public record GetAllTaskQuery(int AccountId) : IRequest<List<Domain.Entities.Task>>;
