using BackendSBI.Models;
using Microsoft.Extensions.Options;
using MimeKit;
//using System.Net.Mail;
using MailKit.Net.Smtp;

namespace BackendSBI.Repository
{
    public class SBIRepository : ISBIRepository
    {
        private readonly MailSettings _mailSettings;

        public SBIRepository(IOptions<MailSettings> mailSettingsOptions)
        {
            _mailSettings = mailSettingsOptions.Value;
        }
        public async Task SendEmailAsync(string emailAddress, string subject, string content)
        {
            try
            {
                var message = new MimeMessage();
                message.From.Add(new MailboxAddress(_mailSettings.DisplayName, _mailSettings.Mail)); // Replace with your Mailtrap email
                message.To.Add(new MailboxAddress("", emailAddress));
                message.Subject = subject;

                var bodyBuilder = new BodyBuilder();
                bodyBuilder.TextBody = content;

                message.Body = bodyBuilder.ToMessageBody();

                using (var client = new SmtpClient())
                {
                    await client.ConnectAsync(_mailSettings.Host, 587, false); // Replace with Mailtrap SMTP server and port
                    await client.AuthenticateAsync(_mailSettings.UserName, _mailSettings.Password); // Replace with your Mailtrap credentials
                    await client.SendAsync(message);
                    await client.DisconnectAsync(true);
                }
            }
            catch (Exception ex)
            {
                // Handle email sending exceptions...
                throw;
            }
        }
    }
}
