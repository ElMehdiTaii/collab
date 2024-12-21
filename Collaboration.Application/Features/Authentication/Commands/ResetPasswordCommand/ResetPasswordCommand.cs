using Collaboration.Domain.DTOs.Common;
using MediatR;

namespace Collaboration.Application.Features.Authentication.Commands.ResetPasswordCommand;

public record ResetPasswordCommand(string Email, string Password) :
    IRequest<Response>;
