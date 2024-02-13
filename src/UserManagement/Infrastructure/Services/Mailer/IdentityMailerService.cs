using MediatR;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.Extensions.Logging;
using SharedKernel.Interfaces;
using SharedKernel.Models;
using SharedKernel.Services;

namespace UserManagement.Infrastructure.Services.Mailer;

public class IdentityMailerService : IMailerService
{
    private readonly ILogger<DomainEventService> _logger;
    private readonly IEmailSender _emailSender;

    public IdentityMailerService(ILogger<DomainEventService> logger, IEmailSender emailSender)
    {
        _logger = logger;
        _emailSender = emailSender;
    }

    public async Task SendEmailAsync(string to, string from, string subject, string body)
    {
        _logger.LogInformation($"Email with subject {subject} sent to email address: {to}");
        await _emailSender.SendEmailAsync(to, subject, body);
    }
}