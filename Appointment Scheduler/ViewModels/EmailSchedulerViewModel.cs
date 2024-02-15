using Appointment_Scheduler.Models;

namespace Appointment_Scheduler.ViewModels
{
    public class EmailSchedulerViewModel
    {
        public string EmailToId { get; set; } = default!;
        public string EmailToName { get; set; } = default!;
        public string EmailSubject { get; set; } = default!;
        public string EmailBody { get; set; } = default!;
        public int delay { get; set; } = default;
        public DateTime endDate { get; set; } = default;
    }
}
