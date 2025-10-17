using MailKit.Net.Smtp;
using Microsoft.CodeAnalysis.Options;
using Microsoft.Extensions.Options;
using Microsoft.VisualBasic.FileIO;
using MimeKit;
using System.Security;

namespace ASP.NET.Assignment.PL.Helpers.Services.Mail
{
    public class MailServices(IOptions<MailSettings> _option) : IMailServices
    {
        public bool SendEmail(Email email)
        {
            try
            {
                // Build Message
                var mail = new MimeMessage();
                mail.Subject = email.subject;
                mail.From.Add(new MailboxAddress(_option.Value.DisplayName, _option.Value.Email));
                mail.To.Add(MailboxAddress.Parse(email.To));

                var builder = new BodyBuilder();
                builder.TextBody = email.Body;
                mail.Body = builder.ToMessageBody();
                // Establish Connection
                using var smtp = new SmtpClient();
                smtp.Connect(_option.Value.Host, _option.Value.Port, MailKit.Security.SecureSocketOptions.StartTls);
                smtp.Authenticate(_option.Value.Email, _option.Value.Password);
                // Send Message
                smtp.Send(mail);
            }
            catch (Exception ex) { 
                return false;
            }
            return true;
        }
    }
}
