using FluentValidation;

namespace Collaboration.Application.Features.Board.Commands.CreateBoardCommand;

public class CreateBoardCommandValidator : AbstractValidator<CreateBoardCommand>
{
    public CreateBoardCommandValidator()
    {
        RuleFor(p => p.Title)
            .NotNull()
            .NotEmpty()
            .WithMessage("");
    }
}
