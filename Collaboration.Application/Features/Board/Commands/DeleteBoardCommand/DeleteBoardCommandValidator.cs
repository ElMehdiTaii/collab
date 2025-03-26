using FluentValidation;

namespace Collaboration.Application.Features.Board.Commands.DeleteBoardCommand;

internal class DeleteBoardCommandValidator : AbstractValidator<DeleteBoardCommand>
{
    public DeleteBoardCommandValidator()
    {
        RuleFor(p => p.Id)
        .NotNull()
        .NotEmpty()
        .WithMessage("");
    }
}