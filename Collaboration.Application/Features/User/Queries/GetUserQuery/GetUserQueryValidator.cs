using FluentValidation;

namespace Collaboration.Application.Features.User.Queries.GetUserQuery;

public class GetUserQueryValidator : AbstractValidator<GetUserQuery>
{
    public GetUserQueryValidator()
    {
        RuleFor(p => p.AccountId)
           .NotNull()
           .NotEmpty()
           .WithMessage("");
    }
}
