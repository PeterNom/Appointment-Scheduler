using Appointment_Scheduler.Models;
using Appointment_Scheduler.Repositories;
using Appointment_Scheduler.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel;
using System.Net.NetworkInformation;
using static Appointment_Scheduler.Models.Appointment;
using System.Security.Claims;
using Microsoft.AspNetCore.Identity;

namespace Appointment_Scheduler.Controllers
{
    
    [Authorize]
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
                               select new { ID = (int)e, Name = e.ToString(), };
                
                IEnumerable<SelectListItem> selectListItems = new SelectList(enumData, "ID", "Name");
                
                AppointmentAddViewModel appointmentAddViewModel = new() { ReminderInTime = selectListItems };

                var count = await _appointmentRepository.GetAllAppointmentCountAsync();

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

                EmailSchedulerViewModel emailSchedulerViewModel = new EmailSchedulerViewModel() 
                { EmailToId = appointment.Email, EmailToName = appointment.Appointment_Name, EmailSubject ="Appointment Reminder"
                , EmailBody = "<html><body> Your Appointment" + appointment.Appointment_Name + " </body></html>", delay = (int)appointment.Reminder,
                    endDate = appointment.EndDate
                };

                using (var httpClient = new HttpClient())
                {
                    using( var response = await httpClient.PostAsJsonAsync("https://localhost:7005/api/mail/", emailSchedulerViewModel)  )
                    {
                        await response.Content.ReadAsStringAsync();
                    }
                }

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

        public async Task<IActionResult> Update(int? id)
        {
            if(id == null)
            {
                return NotFound();
            }

            var enumData = from ReminderIn e in Enum.GetValues(typeof(ReminderIn)) 
                           select new { ID = (int)e, Name = e.ToString(), };
            
            IEnumerable<SelectListItem> selectListItems = new SelectList(enumData, "ID", "Name");

            var selectedAppointment = await _appointmentRepository.GetAppointmentByIdAsync(id.Value);

            AppointmentUpdateViewModel appointmentUpdateViewModel = new() { ReminderInTime = selectListItems, Appointment = selectedAppointment };

            return View(appointmentUpdateViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(AppointmentUpdateViewModel app)
        {
            var appointment = await _appointmentRepository.GetAppointmentByIdAsync(app.Appointment.AppointmentId);

            try
            {
                if (appointment == null)
                {
                    ModelState.AddModelError(string.Empty, "The appointment you want to update doesn't exist or was already deleted by someone else.");
                }

                if(ModelState.IsValid)
                {
                    await _appointmentRepository.UpdateAppointmentsAsync(app.Appointment);
                    
                    return RedirectToAction(nameof(Index));
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", $"Updating the appointment failed, please try again! Error: {ex.Message}");
            }

            var enumData = from ReminderIn e in Enum.GetValues(typeof(ReminderIn))
                           select new { ID = (int)e, Name = e.ToString(), };

            IEnumerable<SelectListItem> selectListItems = new SelectList(enumData, "ID", "Name");

            var selectedAppointment = await _appointmentRepository.GetAppointmentByIdAsync(app.Appointment.AppointmentId);

            AppointmentUpdateViewModel appointmentUpdateViewModel = new() { ReminderInTime = selectListItems, Appointment = selectedAppointment };

            return View(appointmentUpdateViewModel);
        }

        public IActionResult DeleteDue()
        {
            return View( _appointmentRepository.GetAllDueAppointmentsAsync());
        }

        [HttpPost, ActionName("DeleteDue")]
        public async Task<IActionResult> DeleteDueConfirm()
        {
            try
            {
                await _appointmentRepository.DeleteCompletedAppointmentsAsync();

                TempData["DueAppointmentsDeleted"] = "Due Appointments deleted successfully!";

                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ViewData["ErrorMessage"] = $"Deleting due appointments failed, please try again! Error: {ex.Message}";
            }

            return View(_appointmentRepository.GetDueAppointmentsAsync());
        }

        public IActionResult Search()
        { 
            return View(); 
        }
    }
}
