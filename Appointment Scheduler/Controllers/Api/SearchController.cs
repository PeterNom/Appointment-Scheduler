using Appointment_Scheduler.Models;
using Appointment_Scheduler.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Appointment_Scheduler.Controllers.Api
{
    [Route("api/[controller]")]
    [ApiController]
    public class SearchController : ControllerBase
    {
        private readonly IAppointmentRepository _appointmentRepository;

        public SearchController(IAppointmentRepository appointmentRepository)
        {
            _appointmentRepository = appointmentRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var allApp = await _appointmentRepository.GetAllAppointmentsAsync();

            return Ok(allApp);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var allApp = await _appointmentRepository.GetAppointmentByIdAsync(id);
            
            if (allApp == null)
            {
                return NotFound();
            }
            return Ok(allApp);
        }

        [HttpPost]
        public IActionResult SearchAppointments([FromBody] string searchQuery)
        {
            IEnumerable<Appointment> appointments = new List<Appointment>();

            if(!string.IsNullOrEmpty(searchQuery))
            {
                appointments = _appointmentRepository.SearchAppointments(searchQuery);
            }

            return new JsonResult(appointments);
        }
    }
}
