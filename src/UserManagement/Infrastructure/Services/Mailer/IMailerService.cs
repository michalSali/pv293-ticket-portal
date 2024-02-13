namespace UserManagement.Infrastructure.Services.Mailer;

public interface IMailerService
{
    Task SendEmailAsync(string to, string from, string subject, string body);
}