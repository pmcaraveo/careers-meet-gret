using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace MyAlfaLive.Domain.Services
{
    public class EmailServiceUsers
    {
        // GET: EmailService
        private readonly string host;
        private readonly int port;
        private readonly string userName;
        //private readonly string password;
        private readonly bool enableSSL;

        public EmailServiceUsers(string host, int port, bool enableSSL, string userName)
        {
            this.host = host;
            this.port = port;
            this.enableSSL = enableSSL;
            this.userName = userName;
            //this.password = password;
        }

        public Task SendEmailAsuncUsers(string email, string subject, string htmlMessage)
        {
            var client = new SmtpClient(host, port)
            {
                Crendentials = new NetworkCredential (userName),
                EnableSsl = enableSSL
            };
            return client.SendMailAsync(
                new MailMessage(userName, email, subject, htmlMessage) { IsBodyHtml = true }
            );
        }
    }

}
