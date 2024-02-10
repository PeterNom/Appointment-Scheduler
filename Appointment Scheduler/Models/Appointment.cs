using System.ComponentModel.DataAnnotations;

namespace Appointment_Scheduler.Models
{
    public class Appointment
    {
        public enum ReminderIn
        {
            None,
            HalfHour,
            OneHour,
            TwoHours, 
            FourHours,
            SixHours,
            TwelveHours,
            TwentyFourHours,
            OneWeek
        }

        public int AppointmentId { get; set; }

        [Display(Name = "Appointment Name")]
        [Required]
        public string Appointment_Name { get; set; } = string.Empty;

        [StringLength(100)]
        public string? Appointment_Description { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime StartDate { get; set;}
        public DateTime EndDate { get; set; }

        [Display(Name = "Location")]
        public string? Location { get; set; }
        public ReminderIn Reminder { get; set; }

        [Display(Name = "Phone Number")]
        [Required]
        public string PhoneNumber { get; set; } = string.Empty;

        [Display(Name = "Email")]
        [Required]
        public string Email { get; set; } = string.Empty;
    }
}
