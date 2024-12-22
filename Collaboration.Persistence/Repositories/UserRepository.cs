using Collaboration.Application.Contracts.Persistence;
using Collaboration.Domain.Entities;
using Collaboration.Persistence.DatabaseContext;
using Microsoft.EntityFrameworkCore;

namespace Collaboration.Persistence.Repositories;

public class UserRepository(CollaborationDatabaseContext context) : GenericRepository<User>(context), IUserRepository
{
    public async Task<User?> GetAsync(string email)
    {
        return await _context.User
            .Include(a => a.Account)
            .FirstOrDefaultAsync(u => u.Email == email);
    }
}
