using Collaboration.Application.Contracts.Persistence;
using Collaboration.Application.Exceptions;
using Collaboration.Domain.DTOs.Common;
using MediatR;

namespace Collaboration.Application.Features.Board.Commands.CreateBoardCommand;

public class CreateBoardCommandHandler(IBoardRepository boardRepository, ITaskRepository taskRepository) : IRequestHandler<CreateBoardCommand, Response>
{
    public async Task<Response> Handle(CreateBoardCommand request, CancellationToken cancellationToken)
    {
        var tasks = (await taskRepository.GetTasksAsync(request.TasksId))
            .Select(task =>
            {
                task.Status = (int)Domain.Enums.TaskStatus.NEW;
                return task;
            }).ToList();

        var board = new Domain.Entities.Board
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
