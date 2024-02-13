using Appointment_Scheduler.Models;

namespace Appointment_Scheduler.ViewModels
{
    public class HomeIndexViewModel
    {
        public IEnumerable<Appointment>? Appointments { get; set; }
    }
}
