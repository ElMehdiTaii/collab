using FluentValidation;

namespace Collaboration.Application.Features.Authentication.Commands.SendResetPasswordMailCommand;

public class SendResetPasswordCommandValidator :
    AbstractValidator<SendResetPasswordCommand>
{
    public SendResetPasswordCommandValidator()
    {
        RuleFor(p => p.Email)
            .NotNull()
            .NotEmpty()
            .WithMessage("");
    }
}