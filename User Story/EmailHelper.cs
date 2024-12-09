using System;
using System.Net.Mail;

namespace User_Story
{
    public static class EmailHelper
    {
        public static void SendEmail(string to, string subject, string body)
        {
            using (MailMessage mail = new MailMessage())
            {
                // Set sender's email
                mail.From = new MailAddress("testingthesystem678@gmail.com");

                // Set recipient email
                mail.To.Add(to);

                // Email subject and body
                mail.Subject = subject;
                mail.Body = body;
                mail.IsBodyHtml = true; // If HTML content is required

                // Configure Gmail SMTP settings
                using (SmtpClient smtp = new SmtpClient("smtp.gmail.com", 587))
                {
                    // Credentials
                    smtp.Credentials = new System.Net.NetworkCredential("testingthesystem678@gmail.com", "Theworldisyours1_");

                    // Enable SSL for secure communication
                    smtp.EnableSsl = true;

                    // Send the email
                    smtp.Send(mail);
                }
            }
        }
    }
}
