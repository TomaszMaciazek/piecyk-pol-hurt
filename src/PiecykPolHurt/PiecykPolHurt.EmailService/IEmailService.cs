namespace PiecykPolHurt.EmailService
{
    public interface IEmailService
    {
        Task SendEmail(SendEmailRequest sendEmailRequest);
    }
}
