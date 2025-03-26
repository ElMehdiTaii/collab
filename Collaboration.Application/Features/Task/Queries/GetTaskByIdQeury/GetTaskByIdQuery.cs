using MediatR;

namespace Collaboration.Application.Features.Task.Queries.GetTaskByIdQeury;

public record GetTaskByIdQuery(int Id) : IRequest<Domain.Entities.Task>;
