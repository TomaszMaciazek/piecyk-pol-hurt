namespace PiecykPolHurt.Email
{
    public interface IEmailConfiguration
    {
        string Host { get; set; }
        string Password { get; set; }
        string User { get; set; }
    }

    public class EmailConfiguration : IEmailConfiguration
    {
        public string Host { get; set; }
        public string User { get; set; }
        public string Password { get; set; }

        public EmailConfiguration(string host, string user, string password)
        {
            Host = host;
            User = user;
            Password = password;
        }
    }
}
