using Appointment_Scheduler.Models;
using Appointment_Scheduler.Repositories;
using Appointment_Scheduler.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Appointment_Scheduler.Controllers
{
    public class HomeController : Controller
    {
        private readonly IAppointmentRepository _appointmentRepository;

        public HomeController(IAppointmentRepository appointmentRepository)
        {
            _appointmentRepository = appointmentRepository;
        }
        [Authorize]
        public async Task<IActionResult> Index()
        {
            IEnumerable<Appointment> appointmentsDue = await _appointmentRepository.GetDueAppointmentsAsync();

            HomeIndexViewModel homeViewModel = new() { Appointments = appointmentsDue };

            return View(homeViewModel);
        }
    }
}
