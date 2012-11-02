using System;
using System.Net.Mail;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net;
using System.Security.Cryptography.X509Certificates;
using System.Net.Security;

namespace Varldsklass.Web.Infrastructure
{
    public class SendMail
    {
        SmtpClient smtpClient;

        public SendMail()
        {
            smtpClient = new SmtpClient();

            smtpClient.EnableSsl = true;
            smtpClient.Host = "smtp.varldsklass.com";
            smtpClient.Port = 587;
            smtpClient.UseDefaultCredentials = false;
            smtpClient.Credentials = new NetworkCredential("noreply@varldsklass.com", "LÖSENORD HÄR");
        }

        public void send( string from, string to, string subject, string body )
        {
            MailMessage mailMessage = new MailMessage(
                from,
                to,
                subject,
                body
            );
            ServicePointManager.ServerCertificateValidationCallback = delegate(object s, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors) { return true; };
            smtpClient.Send(mailMessage);
        }
    }
}