using System.Net;
using System.Net.Mail;

namespace PiecykPolHurt.Email
{
    public interface IEmailService
    {
        Task<bool> SendEmail(string email, string subject, string body);
    }

    public class EmailService : IEmailService
    {
        private readonly IEmailConfiguration _configuration;

        public EmailService(IEmailConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task<bool> SendEmail(string email, string subject, string body)
        {
            try
            {
                var fromAddress = new MailAddress(_configuration.User, "Piecyk Pol Hurt");
                var toAddress = new MailAddress(email);
                var client = new SmtpClient
                {
                    Host = _configuration.Host,
                    Port = 587,
                    EnableSsl = true,
                    DeliveryMethod = SmtpDeliveryMethod.Network,
                    UseDefaultCredentials = false,
                    Credentials = new NetworkCredential(fromAddress.Address, _configuration.Password)
                };

                using (var message = new MailMessage(fromAddress, toAddress)
                {
                    Body = body,
                    Subject = subject
                }
                )
                {
                    await client.SendMailAsync(message);
                }
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
