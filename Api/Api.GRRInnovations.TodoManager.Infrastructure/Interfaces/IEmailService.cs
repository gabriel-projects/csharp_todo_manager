namespace Api.GRRInnovations.TodoManager.Infrastructure.Interfaces
{
    public interface IEmailService
    {
        Task SendEmailAsync(string toEmail, string subject, string message);
    }
}
