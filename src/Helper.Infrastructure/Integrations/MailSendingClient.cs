using SendGrid;
using SendGrid.Helpers.Mail;

namespace Helper.Infrastructure.Integrations
{
    public class MailSendingClient
    {
        private readonly SendGridClient _mailClient;

        public MailSendingClient()
        {
            var apiKey = Environment.GetEnvironmentVariable("SENDGRID_API_KEY");
            _mailClient = new SendGridClient(apiKey);
        }

        public async Task SendMail() 
        {
            var msg = new SendGridMessage()
            {
                From = new EmailAddress("[REPLACE WITH YOUR EMAIL]", "[REPLACE WITH YOUR NAME]"),
                Subject = "Sending with Twilio SendGrid is Fun",
                PlainTextContent = "and easy to do anywhere, especially with C#"
            };
            msg.AddTo(new EmailAddress("[REPLACE WITH DESIRED TO EMAIL]", "[REPLACE WITH DESIRED TO NAME]"));
            var response = await _mailClient.SendEmailAsync(msg);
        }
    }
}
