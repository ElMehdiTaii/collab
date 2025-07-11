﻿using Collaboration.Application.Contracts.Email;
using Collaboration.Domain.Models;
using Microsoft.Extensions.Options;
using SendGrid;
using SendGrid.Helpers.Mail;

namespace Collaboration.Infrastructure.Email;

public class EmailSender(IOptions<EmailSettings> emailSettings) : IEmailSender
{
    public EmailSettings _emailSettings { get; } = emailSettings.Value;

    public async Task<bool> SendEmail(EmailMessage email)
    {
        var client = new SendGridClient(_emailSettings.ApiKey);

        var to = new EmailAddress(email.To);

        var from = new EmailAddress
        {
            Email = _emailSettings.FromAddress,
            Name = _emailSettings.FromName
        };

        var message = MailHelper.CreateSingleEmail(
            from,
            to,
            email.Subject,
            email.Body,
            email.Body);

        var response = await client.SendEmailAsync(message);

        return response.IsSuccessStatusCode;
    }
}
