using Microsoft.AspNetCore.Identity;
using System.Net;
using System.Security.Authentication;
using System.Security.Policy;
using System.Text;
using MimeKit;
using Tashtibaat.Models;
using MailKit.Net.Smtp;

namespace Tashtibaat.Helpers
{
    public class SendMail : ISendMail
    {
        public bool Send(string ReceiverUserName, string ReceiverEmail, string Link, string Subject)
        {
            //var Code = await userManager.GenerateEmailConfirmationTokenAsync(user);
            //var Link = Url.Action(nameof(VerifyEmail), "Account", new { UserId = user.Id, Code }, Request.Scheme);

            var message = new MimeMessage();
            message.From.Add(new MailboxAddress("RedZone", "abdooooahmed7@gmail.com"));
            message.To.Add(new MailboxAddress(ReceiverUserName, ReceiverEmail));
            message.Subject = Subject;
            message.Body = new TextPart("html")
            {
                Text = $"<a href=\"{Link}\">Link</a>"
            };
            using (var client = new SmtpClient())
            {
                client.SslProtocols = SslProtocols.Ssl3 | SslProtocols.Ssl2 | SslProtocols.Tls | SslProtocols.Tls11 | SslProtocols.Tls12 | SslProtocols.Tls13;
                client.CheckCertificateRevocation = false;
                client.Connect("smtp.gmail.com", 465, true);
                client.Authenticate("abdooooahmed7@gmail.com", "seehpadpivwlpwqm");
                client.Send(message);
                client.Disconnect(true);
                return true;
            }
        }

        public bool Send(Users user, string Subject, string Text)
        {
            var message = new MimeMessage();
            message.From.Add(new MailboxAddress("RedZone", "abdooooahmed7@gmail.com"));
            message.To.Add(new MailboxAddress("abdo", "abdooooahmed7@gmail.com"));
            message.Subject = Subject;
            message.Body = new TextPart("html")
            {
                Text = $"<p>text {user.PhoneNumber}</p>"
            };
            using (var client = new SmtpClient())
            {
                client.SslProtocols = SslProtocols.Ssl3 | SslProtocols.Ssl2 | SslProtocols.Tls | SslProtocols.Tls11 | SslProtocols.Tls12 | SslProtocols.Tls13;
                client.CheckCertificateRevocation = false;
                client.Connect("smtp.gmail.com", 465, true);
                client.Authenticate("abdooooahmed7@gmail.com", "seehpadpivwlpwqm");
                client.Send(message);
                client.Disconnect(true);
                return true;
            }
        }
    }
}
