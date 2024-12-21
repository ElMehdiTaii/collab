using FluentValidation;

namespace Collaboration.Application.Features.Authentication.Queries.AuthenticationQuery;

public class AuthenticationQueryValidator : AbstractValidator<AuthenticationQuery>
{
    public AuthenticationQueryValidator()
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
