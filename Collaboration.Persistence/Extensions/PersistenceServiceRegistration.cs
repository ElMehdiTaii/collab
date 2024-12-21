using Collaboration.Application.Contracts.Persistence;
using Collaboration.Persistence.DatabaseContext;
using Collaboration.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Collaboration.Persistence.Extensions;
public static class PersistenceServiceRegistration
{
    public static IServiceCollection AddPersistenceServices(this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddDbContext<CollaborationDatabaseContext>(options =>
        {
            options.UseSqlServer(configuration.GetConnectionString(""));
        });

        services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));

        services.AddScoped<IUserRepository, UserRepository>();

        return services;
    }
}