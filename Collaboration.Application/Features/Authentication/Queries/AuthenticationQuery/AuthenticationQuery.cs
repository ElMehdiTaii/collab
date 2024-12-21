using Collaboration.Domain.DTOs.Common;
using MediatR;

namespace Collaboration.Application.Features.Authentication.Queries.AuthenticationQuery;

public record AuthenticationQuery(string Email, string Password) :
    IRequest<Response>;
