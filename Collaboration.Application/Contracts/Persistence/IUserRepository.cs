using Collaboration.Domain.Entities;

namespace Collaboration.Application.Contracts.Persistence;

public interface IUserRepository : IGenericRepository<User>
{
    Task<User?> GetAsync(string email);
    Task<List<User>> GetUsersAsync(int accountId);
}
