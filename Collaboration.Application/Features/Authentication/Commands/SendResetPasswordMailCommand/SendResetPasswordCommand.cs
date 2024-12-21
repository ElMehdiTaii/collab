using Collaboration.Domain.DTOs.Common;
using MediatR;

namespace Collaboration.Application.Features.Authentication.Commands.SendResetPasswordMailCommand;
public record SendResetPasswordCommand(string Email) :
    IRequest<Response>;
