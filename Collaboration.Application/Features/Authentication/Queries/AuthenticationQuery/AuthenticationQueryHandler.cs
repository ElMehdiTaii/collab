using Collaboration.Application.Contracts.Email;
using Collaboration.Application.Contracts.HashingPassword;
using Collaboration.Application.Contracts.Persistence;
using Collaboration.Application.Exceptions;
using Collaboration.Domain.Constants;
using Collaboration.Domain.DTOs.Common;
using MediatR;

namespace Collaboration.Application.Features.Authentication.Queries.AuthenticationQuery;

public class AuthenticationQueryHandler(IUserRepository userRepository, IPasswordHasherService passwordHasherService, IEmailSender emailSender) :
    IRequestHandler<AuthenticationQuery, Response>
{
    public async Task<Response> Handle(AuthenticationQuery request, CancellationToken cancellationToken)
    {
        Domain.Entities.User user = await userRepository.GetAsync(request.Email) ??
            throw new BadRequestException("Adresse email ou mot de passe invalide. Veuillez réessayer.");

        if (!passwordHasherService.VerifyPasswordHash(request.Password, user.PasswordHash!, user.PasswordSalt!))
        {
            throw new BadRequestException("Adresse email ou mot de passe invalide. Veuillez réessayer.");
        }

        await emailSender.SendEmail(new Domain.Models.EmailMessage
        {
            To = request.Email,
            Subject = Constant.SendEmailRestPasswordSucceedObject,
            Body = Constant.SendEmailRestPasswordSucceedBody
        });

        return new Response(string.Empty, user);
    }
}
