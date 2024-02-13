using Appointment_Scheduler.Configuration;
using Appointment_Scheduler.Models;
using Microsoft.Extensions.Options;
using System.Net;
using System.Net.Mail;

namespace Appointment_Scheduler.Services
{
    public class MailService : IMailService
    {
        private readonly MailSettings _mailSettings;

        public MailService(IOptions<MailSettings> mailSettingsOptions)
        {
            _mailSettings = mailSettingsOptions.Value;
        }
        public bool SendMail(MailData mailData)
        {
            try
            {
                MailMessage message = new MailMessage();

                message.From = new MailAddress(_mailSettings.SenderEmail);
                message.Subject = mailData.EmailSubject;
                message.To.Add(new MailAddress(mailData.EmailToId));
                message.Body = mailData.EmailBody;
                message.IsBodyHtml = true;

                using SmtpClient smtpClient = new SmtpClient(_mailSettings.Server)
                {
                    Port = _mailSettings.Port,
                    Credentials = new NetworkCredential(_mailSettings.SenderEmail, _mailSettings.Password),
                    EnableSsl = true,
                };

                smtpClient.Send(message);

                return true;
            }
            catch (Exception ex)
            {
                ex.ToString();
                return false;
            }
        }
    }
}
