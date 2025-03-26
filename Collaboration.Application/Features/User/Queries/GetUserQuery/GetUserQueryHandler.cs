using Collaboration.Application.Contracts.Persistence;
using MediatR;

namespace Collaboration.Application.Features.User.Queries.GetUserQuery;

public sealed class GetUserQueryHandler(IUserRepository userRepository) : IRequestHandler<GetUserQuery, List<Domain.Entities.User>>
{
    public async Task<List<Domain.Entities.User>> Handle(GetUserQuery request, CancellationToken cancellationToken)
    {
        return await userRepository.GetUsersAsync(request.AccountId);
    }
}
