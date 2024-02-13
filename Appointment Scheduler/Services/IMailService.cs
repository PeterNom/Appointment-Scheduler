using Appointment_Scheduler.Models;

namespace Appointment_Scheduler.Services
{
    public interface IMailService
    {
        bool SendMail(MailData mailData);
    }
}
