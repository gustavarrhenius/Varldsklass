using System;
using System.Net.Mail;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net;
using System.Security.Cryptography.X509Certificates;
using System.Net.Security;
using System.Text;
using RazorEngine;
using System.IO;
using System.Configuration;

namespace Varldsklass.Web.Infrastructure
{
    public class SendMail
    {
        string senderAddress;
        SmtpClient smtpClient;

        public SendMail()
        {
            senderAddress = ConfigurationManager.AppSettings["senderAddress"];
            string mailPassword = ConfigurationManager.AppSettings["mailPassword"];

            smtpClient = new SmtpClient();

            smtpClient.EnableSsl = true;
            smtpClient.Host = "smtp.varldsklass.com";
            smtpClient.Port = 587;
            smtpClient.UseDefaultCredentials = false;
            smtpClient.Credentials = new NetworkCredential(senderAddress, mailPassword);
        }

        public string render(string templateName, dynamic model)
        {
            string templatePath = ConfigurationManager.AppSettings[templateName];
            string fullTemplatePath = System.IO.Path.Combine(System.AppDomain.CurrentDomain.BaseDirectory, templatePath);
            var template = File.ReadAllText(fullTemplatePath);
            string body = Razor.Parse(template, model);
            return body;
        }

        public void send(string to, string subject, string body)
        {
            MailMessage mailMessage = new MailMessage(
                senderAddress,
                to,
                subject,
                body
            );

            mailMessage.BodyEncoding = Encoding.UTF8;
            mailMessage.IsBodyHtml = true;

            // Hack for allowing us to send mail with some providers
            ServicePointManager.ServerCertificateValidationCallback = delegate(object s, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors) { return true; };

            smtpClient.Send(mailMessage);
        }
    }
}