using MediatR;

namespace Collaboration.Application.Features.User.Queries.GetUserQuery;

public sealed record GetUserQuery(int AccountId) : IRequest<List<Domain.Entities.User>>;
