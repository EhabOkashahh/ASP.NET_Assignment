using Microsoft.EntityFrameworkCore.SqlServer.Query.Internal;
using System.Net;
using System.Net.Mail;

namespace ASP.NET.Assignment.PL.Helpers
{
    public static class EmailSettings
    {
        public static bool SendEmail(Email email)
        {
            // Mail Server
            // Protocol => SMTP(Simple Mail Transfer Protocol)
            try
            {
                var client = new SmtpClient("smtp.gmail.com", 587);
                client.EnableSsl = true;
                client.Credentials = new NetworkCredential("okashahehab8@gmail.com", "pdcjueggyfcomcmk"); // => sender
                client.Send("okashahehab8@gmail.com", email.To, email.subject, email.Body);
            }
            catch (Exception ex) {

                return false;
            }
            return true;
        }
    } 
}
