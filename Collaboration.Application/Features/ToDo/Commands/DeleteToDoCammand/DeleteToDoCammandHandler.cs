using MediatR;

namespace Collaboration.Application.Features.ToDo.Commands.DeleteToDoCammand;

public record DeleteToDoCammandHandler(int Id) : IRequest<bool>;
