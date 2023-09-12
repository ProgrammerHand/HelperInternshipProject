using Helper.Application.Integrations;
using MailKit.Net.Smtp;
using Microsoft.Extensions.Configuration;
using MimeKit;
using System.Net;

namespace Helper.Infrastructure.Integrations
{
    public class MailSendingSmtp : IMailSendingClient
    {
        private readonly IConfiguration _configuration;

        public MailSendingSmtp(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        private SmtpClient GetMailSendingSMTP()
        {
            var smtp = new SmtpClient();
            smtp.Connect("smtp.gmail.com", 587, false);
            smtp.Authenticate(_configuration.GetValue<string>("projectMail:adress"), _configuration.GetValue<string>("projectMail:password"));
            return smtp;
        }

        public async Task<HttpStatusCode> SendMailAsync(MailDto data)
        {
            if (data.ReciverEmail.Trim().EndsWith("."))
            {
                var email = new MimeMessage();
                email.From.Add(new MailboxAddress(_configuration.GetValue<string>("app:name"), _configuration.GetValue<string>("projectMail:adress")));
                email.To.Add(new MailboxAddress(data.ReciverName, data.ReciverEmail));
                var builder = new BodyBuilder();
                builder.HtmlBody = data.Content;
                email.Subject = data.Subject;


                if (data.Attachment is not null)
                {
                    builder.Attachments.Add($"{data.AttachmentName}.{data.AttachmentType}", data.Attachment, new ContentType("application", data.AttachmentType));
                }

                email.Body = builder.ToMessageBody();

                using (var smtp = GetMailSendingSMTP())
                {
                    //smtp.Connect("smtp.gmail.com", 587, false);
                    await smtp.SendAsync(email);
                    smtp.Disconnect(true);
                }
                return HttpStatusCode.OK;
            }
            return HttpStatusCode.NoContent;
        }
    }
}
