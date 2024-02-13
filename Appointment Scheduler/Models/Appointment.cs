using Microsoft.AspNetCore.Identity;
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
        [Display(Name = "Short description")]
        public string? Appointment_Description { get; set; }

        public DateTime CreatedDate { get; set; }

        [Display(Name = "Start Date")]
        public DateTime StartDate { get; set;}

        [Display(Name = "End Date")]
        public DateTime EndDate { get; set; }

        [Display(Name = "Location")]
        public string? Location { get; set; }

        public ReminderIn Reminder { get; set; }

        [Required]
        [StringLength(10)]
        [DataType(DataType.PhoneNumber)]
        [Display(Name = "Phone Number")]
        public string PhoneNumber { get; set; } = string.Empty;

        [Required]
        [StringLength(50)]
        [DataType(DataType.EmailAddress)]
        [RegularExpression(@"(?:[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*|""(?:[\x01-\x08\x0b\x0c\x0e-\x1f\x21\x23-\x5b\x5d-\x7f]|\\[\x01-\x09\x0b\x0c\x0e-\x7f])*"")@(?:(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?|\[(?:(?:25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)\.){3}(?:25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?|[a-z0-9-]*[a-z0-9]:(?:[\x01-\x08\x0b\x0c\x0e-\x1f\x21-\x5a\x53-\x7f]|\\[\x01-\x09\x0b\x0c\x0e-\x7f])+)\])",
            ErrorMessage = "The email address is not entered in a correct format")]
        public string Email { get; set; } = string.Empty;
    }
}
