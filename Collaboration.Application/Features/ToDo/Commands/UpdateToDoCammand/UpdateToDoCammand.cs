using MediatR;

namespace Collaboration.Application.Features.ToDo.Commands.UpdateToDoCammand;
public record UpdateToDoCammand(Domain.Entities.ToDo toDo) : IRequest<bool>;
