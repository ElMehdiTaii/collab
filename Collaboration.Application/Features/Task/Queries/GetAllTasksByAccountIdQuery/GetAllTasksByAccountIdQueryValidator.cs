using FluentValidation;

namespace Collaboration.Application.Features.Task.Queries.GetAllTasksQuery
{
    public class GetAllTasksByAccountIdQueryValidator : AbstractValidator<GetAllTasksByAccountIdQuery>
    {
        public GetAllTasksByAccountIdQueryValidator()
        {
            RuleFor(p => p.AccountId)
                .NotNull()
                .NotEmpty()
                .WithMessage("");
        }
    }
}
