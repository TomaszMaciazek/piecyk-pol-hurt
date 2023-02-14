using Newtonsoft.Json;
using System.Text;

namespace PiecykPolHurt.EmailService
{
    public class EmailService : IEmailService
    {
        private readonly HttpClient _httpClient;

        public EmailService(string url)
        {
            _httpClient = new HttpClient();
            _httpClient.BaseAddress = new Uri(url);
        }

        public async Task SendEmail(SendEmailRequest sendEmailRequest)
        {
            var content = new StringContent(JsonConvert.SerializeObject(sendEmailRequest), Encoding.UTF8, "application/json");
            await _httpClient.PostAsync("", content);
        }
    }
}
