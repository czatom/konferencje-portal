using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace Konferencja
{
    public static class Mail
    {
        public static void Send(string from, string to, string subject, bool isHTML, string message)
        {
            using (MailMessage mm = new MailMessage(from, to))
            {
                mm.Subject = subject;
                mm.Body = message;
                mm.IsBodyHtml = isHTML;
                SmtpClient smtp = new SmtpClient();
                smtp.Host = "smtp.gmail.com";
                smtp.EnableSsl = true;
                NetworkCredential NetworkCred = new NetworkCredential("konferencje.agh@gmail.com", "konfagh8");
                smtp.UseDefaultCredentials = false;
                smtp.Credentials = NetworkCred;
                smtp.Port = 587;
                //await smtp.SendMailAsync(mm);
                smtp.Send(mm);
            }
        }
    }
}
