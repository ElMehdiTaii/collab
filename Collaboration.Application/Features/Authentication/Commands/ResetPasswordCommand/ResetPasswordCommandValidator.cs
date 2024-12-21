using FluentValidation;

namespace Collaboration.Application.Features.Authentication.Commands.ResetPasswordCommand;

public class ResetPasswordCommandValidator :
    AbstractValidator<ResetPasswordCommand>
{
    public ResetPasswordCommandValidator()
    {
        RuleFor(p => p.Email)
            .NotNull()
            .NotEmpty()
            .WithMessage("");

        RuleFor(p => p.Password)
            .NotNull()
            .NotEmpty()
            .WithMessage("");
    }
}
