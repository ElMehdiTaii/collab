using Collaboration.Application.Contracts.Persistence;
using Collaboration.Application.Exceptions;
using Collaboration.Domain.DTOs.Common;
using MediatR;

namespace Collaboration.Application.Features.Board.Commands.CreateBoardCommand;

public class CreateBoardCommandHandler(IBoardRepository boardRepository, ITaskRepository taskRepository) : IRequestHandler<CreateBoardCommand, Response>
{
    public async Task<Response> Handle(CreateBoardCommand request, CancellationToken cancellationToken)
    {
        List<Domain.Entities.Task> tasks = await taskRepository.GetTasksAsync(request.TasksId);

        Domain.Entities.Board board = new()
        {
            Title = request.Title,
            Discription = request.Description,
            AccountId = 1,
            Tasks = tasks
        };

        if (await boardRepository.CreateBoardAsync(board))
            return new Response("Board created successfully", board.Id);
        throw new BadRequestException("Something was wrong");
    }
}
