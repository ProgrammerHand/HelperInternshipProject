using System.Net;

namespace Helper.Application.Integrations
{
    public interface IMailSendingClient
    {
        public Task<HttpStatusCode> SendMailAsync(MailDto data);
    }
}
