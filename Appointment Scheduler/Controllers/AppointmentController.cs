using Appointment_Scheduler.Models;
using Appointment_Scheduler.Repositories;
using Appointment_Scheduler.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel;
using static Appointment_Scheduler.Models.Appointment;

namespace Appointment_Scheduler.Controllers
{
    public class AppointmentController : Controller
    {
        private readonly IAppointmentRepository _appointmentRepository;

        public AppointmentController(IAppointmentRepository appointmentRepository)
        {
            _appointmentRepository = appointmentRepository;
        }

        public async Task<IActionResult> Index()
        {
            IEnumerable<Appointment> appointments;

            appointments = await _appointmentRepository.GetAllAppointmentsAsync();
            
            return View(appointments);
        }

        public async Task<IActionResult> Details(int id)
        {
            var appointment = await _appointmentRepository.GetAppointmentByIdAsync(id);
            
            return View(appointment);
        }

        public async Task<IActionResult> Add()
        {
            try
            {
                var enumData = from ReminderIn e 
                               in Enum.GetValues(typeof(ReminderIn))
                               select new
                               {
                                   ID = (int)e,
                                   Name = e.ToString(),
                               };
                
                IEnumerable<SelectListItem> selectListItems = new SelectList(enumData, "ID", "Name");
                
                AppointmentAddViewModel appointmentAddViewModel = new() { ReminderInTime = selectListItems };

                return View(appointmentAddViewModel);
            }
            catch (Exception ex)
            {
                ViewData["ErrorMessage"] = $"There was an error: ${ex.Message}";
            }
            
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Add(Appointment appointment)
        {
            if(ModelState.IsValid)
            {
                Appointment _appointment = new()
                {
                    Appointment_Name = appointment.Appointment_Name,
                    Appointment_Description = appointment.Appointment_Description,
                    CreatedDate = DateTime.UtcNow,
                    StartDate = appointment.StartDate,
                    EndDate = appointment.EndDate,
                    Location = appointment.Location,
                    Reminder = appointment.Reminder,
                    Email = appointment.Email,
                    PhoneNumber = appointment.PhoneNumber,
                };

                await _appointmentRepository.AddAppointmentsAsync(appointment);

                return RedirectToAction(nameof(Index));
            }

            var enumData = from ReminderIn e in Enum.GetValues(typeof(ReminderIn))
                           select new
                           {
                               ID = (int)e,
                               Name = e.ToString(),
                           };

            IEnumerable<SelectListItem> selectListItems = new SelectList(enumData, "ID", "Name");

            AppointmentAddViewModel appointmentAddViewModel = new() { ReminderInTime = selectListItems };

            return View(appointmentAddViewModel);
        }
        
        public async Task<IActionResult> Delete(int id)
        {
            var appointmentToDelete = await _appointmentRepository.GetAppointmentByIdAsync(id);

            return View(appointmentToDelete);
        }

        [HttpPost,ActionName("Delete")]
        [ValidateAntiForgeryToken]

        public async Task<IActionResult> DeleteConfirmed(int? id)
        {
            if (id == null)
            {
                ViewData["ErrorMessage"] = "Deleting the appointment failed, invalid ID!";
                return View();
            }

            try
            {
                await _appointmentRepository.DeleteAppointmentsAsync(id.Value);
                TempData["AppointmentDeleted"] = "Appointment deleted successfully!";

                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ViewData["ErrorMessage"] = $"Deleting the appointment failed, please try again! Error: {ex.Message}";
            }

            var appointmentToDelete = await _appointmentRepository.GetAppointmentByIdAsync(id.Value);

            return View(appointmentToDelete);
        }
    }
}
