using Appointment_Scheduler.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.IO.Pipelines;

namespace Appointment_Scheduler.ViewModels
{
    public class AppointmentAddViewModel
    {
        public IEnumerable<SelectListItem>? ReminderInTime { get; set; } = default!;
        public Appointment Appointment { get; set; } = default!;
    }
}
