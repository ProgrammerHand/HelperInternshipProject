using Helper.Application.Integrations;
using Microsoft.Extensions.Configuration;
using SendGrid;
using SendGrid.Helpers.Mail;
using System.Net;

namespace Helper.Infrastructure.Integrations
{
    public class MailSendingClient
    {
        private readonly IConfiguration _configuration;

        public MailSendingClient(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        private SendGridClient GetMailSendingClient()
        {
            var apiKey = Environment.GetEnvironmentVariable("SENDGRID_API_KEY");
            return new SendGridClient(apiKey);
        }

        public async Task<HttpStatusCode> SendMailAsync(MailDto data) 
        {
            var mailClient = GetMailSendingClient();
            var msg = new SendGridMessage()
            {
                From = new EmailAddress(_configuration.GetValue<string>("projectMail"), _configuration.GetValue<string>("app:name")),
                Subject = data.Subject,
                PlainTextContent = data.Content
            };
            msg.AddTo(new EmailAddress(data.ReciverEmail, data.ReciverName));
            var response = await mailClient.SendEmailAsync(msg);
            return response.StatusCode;
        }
    }
}
