using MediatR;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.Extensions.Logging;
using SharedKernel.Interfaces;
using SharedKernel.Models;

namespace SharedKernel.Services;

public class MailerService : IMailerService
{
    private readonly ILogger<DomainEventService> _logger;
    private readonly IEmailSender _emailSender;

    public MailerService(ILogger<DomainEventService> logger, IEmailSender emailSender)
    {
        _logger = logger;
        _emailSender = emailSender;
    }

    public async Task SendEmailAsync(string email, string subject, string body)
    {
        _logger.LogInformation($"Email with subject {subject} sent to email address: {email}");
        await _emailSender.SendEmailAsync(subject, subject, body);
    }
}