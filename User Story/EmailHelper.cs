using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace User_Story
{
    public static class EmailHelper
    {
        public static void SendEmail(string to, string subject, string body)
        {
            using (MailMessage mail = new MailMessage())
            {
                mail.From = new MailAddress("your_email@domain.com");
                mail.To.Add(to);
                mail.Subject = subject;
                mail.Body = body;
                mail.IsBodyHtml = true;

                using (SmtpClient smtp = new SmtpClient("smtp.yourdomain.com", 587))
                {
                    smtp.Credentials = new System.Net.NetworkCredential("your_email@domain.com", "your_password");
                    smtp.EnableSsl = true;
                    smtp.Send(mail);
                }
            }
        }
    }

}