using Collaboration.Application.Contracts.Email;
using Collaboration.Application.Contracts.HashingPassword;
using Collaboration.Application.Contracts.Persistence;
using Collaboration.Application.Exceptions;
using Collaboration.Domain.Constants;
using Collaboration.Domain.DTOs.Common;
using MediatR;

namespace Collaboration.Application.Features.Authentication.Commands.ResetPasswordCommand;

public class ResetPasswordCommandHandler(IUserRepository userRepository, IPasswordHasherService passwordHasherService, IEmailSender emailSender) :
    IRequestHandler<ResetPasswordCommand, Response>
{
    public async Task<Response> Handle(ResetPasswordCommand request, CancellationToken cancellationToken)
    {
        Domain.Entities.User user = await userRepository.GetAsync(request.Email) ??
             throw new BadRequestException("Invalid User");

        passwordHasherService.CreatePasswordHash(request.Password, out byte[] passwordHash, out byte[] passwordSalt);

        user.PasswordHash = passwordHash;

        user.PasswordSalt = passwordSalt;

        await userRepository.UpdateAsync(user);

        await emailSender.SendEmail(new Domain.Models.EmailMessage
        {
            To = request.Email,
            Subject = Constant.SendEmailRestPasswordSucceedObject,
            Body = Constant.SendEmailRestPasswordSucceedBody
        });

        return new Response(Constant.ResponseResetPasswordSuccessMessage);
    }
}
