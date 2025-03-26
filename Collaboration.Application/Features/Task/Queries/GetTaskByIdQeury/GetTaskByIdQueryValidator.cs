using FluentValidation;

namespace Collaboration.Application.Features.Task.Queries.GetTaskByIdQeury;

class GetTaskByIdQueryValidator : AbstractValidator<GetTaskByIdQuery>
{
    public GetTaskByIdQueryValidator()
    {
        RuleFor(p => p.Id)
            .NotNull()
            .NotEmpty()
            .WithMessage("");
    }
}