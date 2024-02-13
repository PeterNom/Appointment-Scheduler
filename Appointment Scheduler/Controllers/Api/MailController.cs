using Appointment_Scheduler.Models;
using Appointment_Scheduler.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

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
        public bool SendMail([FromBody] MailData mailData)
        {
            return _mailService.SendMail(mailData);
        }
    }
}
