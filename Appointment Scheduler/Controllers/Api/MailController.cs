using Appointment_Scheduler.Models;
using Appointment_Scheduler.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Hangfire;
using Appointment_Scheduler.ViewModels;

namespace Appointment_Scheduler.Controllers.Api
{
    [Route("api/[controller]")]
    [ApiController]
    public class MailController : ControllerBase
    {
        private readonly IMailService _mailService;

        public MailController(IMailService mailService)
        {
            _mailService = mailService;
        }

        [HttpPost]
        public IActionResult SendMail([FromBody] EmailSchedulerViewModel model)
        {
            MailData mailData = new MailData();

            mailData.EmailToId = model.EmailToId;
            mailData.EmailToName = model.EmailToName;
            mailData.EmailSubject = model.EmailSubject;
            mailData.EmailBody = model.EmailBody;

            switch (model.delay)
            {
                case 1:
                    model.endDate.Subtract(TimeSpan.FromMinutes(30));
                    break;
                case 2:
                    model.endDate = model.endDate.Subtract(TimeSpan.FromHours(1));
                    break;
                case 3:
                    model.endDate.Subtract(TimeSpan.FromHours(2));
                    break;
                case 4:
                    model.endDate.Subtract(TimeSpan.FromHours(4));
                    break;
                case 5:
                    model.endDate.Subtract(TimeSpan.FromHours(6));
                    break;
                case 6:
                    model.endDate.Subtract(TimeSpan.FromHours(12));
                    break;
                case 7:
                    model.endDate.Subtract(TimeSpan.FromHours(24));
                    break;
                case 8:
                    model.endDate.Subtract(TimeSpan.FromDays(7));
                    break;
                default:
                    break;
            }

            BackgroundJob.Schedule(
                () => this._mailService.SendMail(mailData),
                model.endDate
                );
            return Ok();
        }
    }
}
