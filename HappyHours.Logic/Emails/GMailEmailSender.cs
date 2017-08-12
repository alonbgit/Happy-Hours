using HappyHours.Logic.Emails;
using MimeKit;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace HappyHours.Logic.Helpers.Emails
{
    public static class GMailEmailSender
    {
        public static void SendEmail(GmailSendEmailParameters parameters)
        {
            using (SmtpClient smtpClient = new SmtpClient())
            {
                NetworkCredential basicCredential =
                    new NetworkCredential(parameters.From, ConfigHelper.Config.SMTP.Password);
                MailMessage message = new MailMessage();
                MailAddress fromAddress = new MailAddress(parameters.From, parameters.FromDisplayName);
                smtpClient.Host = "smtp.gmail.com";
                smtpClient.Port = 587;
                smtpClient.EnableSsl = true;
                smtpClient.UseDefaultCredentials = false;
                smtpClient.Credentials = basicCredential;
                message.From = fromAddress;
                message.Subject = parameters.Subject;
                message.IsBodyHtml = true;
                message.Body = parameters.Body;
                message.To.Add(parameters.To);
                smtpClient.Send(message);
            }
        }
    }
}
