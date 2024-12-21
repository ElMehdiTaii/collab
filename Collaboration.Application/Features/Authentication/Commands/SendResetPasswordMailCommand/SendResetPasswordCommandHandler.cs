using Collaboration.Application.Contracts.Email;
using Collaboration.Application.Contracts.Persistence;
using Collaboration.Application.Exceptions;
using Collaboration.Domain.Constants;
using Collaboration.Domain.DTOs.Common;
using MediatR;

namespace Collaboration.Application.Features.Authentication.Commands.SendResetPasswordMailCommand;

public class SendResetPasswordCommandHandler(IUserRepository userRepository, IEmailSender emailSender) :
    IRequestHandler<SendResetPasswordCommand, Response>
{
    public async Task<Response> Handle(SendResetPasswordCommand request, CancellationToken cancellationToken)
    {
        Domain.Entities.User user = await userRepository.GetAsync(request.Email) ??
             throw new BadRequestException("Invalid User");


        await emailSender.SendEmail(new Domain.Models.EmailMessage
        {
            To = request.Email,
            Subject = Constant.SendEmailResetPasswordObject,
            Body = Constant.SendEmailResetPasswordBody
        });

        return new Response(Constant.ResponseResetPasswordSendMailSuccessMessage);
    }
}
