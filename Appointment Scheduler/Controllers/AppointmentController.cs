using Appointment_Scheduler.Models;
using Appointment_Scheduler.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace Appointment_Scheduler.Controllers
{
    public class AppointmentController : Controller
    {
        private readonly IAppointmentRepository _appointmentRepository;

        public AppointmentController(IAppointmentRepository appointmentRepository)
        {
            _appointmentRepository = appointmentRepository;
        }

        public IActionResult List()
        {
            IEnumerable<Appointment> appointments;

            appointments = _appointmentRepository.AllAppointments.OrderBy(app => app.StartDate);
            
            return View(appointments);
        }
    }
}
