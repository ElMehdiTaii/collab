using Collaboration.Application.Contracts.Email;
using Collaboration.Application.Contracts.HashingPassword;
using Collaboration.Domain.Models;
using Collaboration.Infrastructure.Email;
using Collaboration.Infrastructure.HashingPassword;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Collaboration.Infrastructure.Extensions;

public static class InfrastructureServicesRegistration
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services)
    {
        //services.Configure<EmailSettings>(configuration.GetSection("EmailSettings"));
        services.AddScoped<IEmailSender, EmailSender>();
        services.AddScoped<IPasswordHasherService, PasswordHasherService>();
        return services;
    }
}
