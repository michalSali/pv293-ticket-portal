namespace SharedKernel.Interfaces;

public interface IMailerService
{
    Task SendEmailAsync(string email, string subject, string body);
}