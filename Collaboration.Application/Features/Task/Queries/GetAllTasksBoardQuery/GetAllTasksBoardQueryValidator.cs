using FluentValidation;

namespace Collaboration.Application.Features.Task.Queries.GetAllTasksBoardQuery;
internal class GetAllTasksBoardQueryValidator : AbstractValidator<GetAllTasksBoardQuery>
{
    public GetAllTasksBoardQueryValidator()
    {
        RuleFor(p => p.BoardId)
        .NotNull()
        .NotEmpty()
        .WithMessage("");
    }
}