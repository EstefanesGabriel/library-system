using System.Net;
using System.Net.Mail;

namespace SistemaBiblioteca.Services
{
    public class EmailService
    {
         public async Task<bool> Send(
            string toName,
            string toEmail,
            string subject,
            string body,
            string fromName = "Equipe Biblioteca",
            string fromEmail = "estefanesgabriel@gmail.com")
        {
            using var smtpClient = new SmtpClient(Configuration.Smtp.Host, Configuration.Smtp.Port)
            {
                Credentials = new NetworkCredential(Configuration.Smtp.UserName, Configuration.Smtp.Password),
                DeliveryMethod = SmtpDeliveryMethod.Network,
                EnableSsl = true
            };

            using var mail = new MailMessage();

            mail.From = new MailAddress(fromEmail, fromName);
            mail.To.Add(new MailAddress(toEmail, toName));
            mail.Subject = subject;
            mail.Body = body;
            mail.IsBodyHtml = true;

            try
            {
                await smtpClient.SendMailAsync(mail);
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"ERRO DE SMTP: {ex.Message}");
                return false;
            }
        }
    }
}