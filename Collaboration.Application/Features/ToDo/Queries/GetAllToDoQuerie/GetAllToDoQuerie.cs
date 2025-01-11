using MediatR;

namespace Collaboration.Application.Features.ToDo.Queries.GetAllToDoQuerie;

public record GetAllToDoQuerie() : IRequest<bool>;
