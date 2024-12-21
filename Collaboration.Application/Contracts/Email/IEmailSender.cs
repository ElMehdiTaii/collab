using Collaboration.Domain.Models;

namespace Collaboration.Application.Contracts.Email;

public interface IEmailSender
{
    Task<bool> SendEmail(EmailMessage email);
}
