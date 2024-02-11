using Appointment_Scheduler.Data;
using static Appointment_Scheduler.Models.Appointment;

namespace Appointment_Scheduler.Models
{
    public class DbInitializer
    {
        public static void Seed(AppointmentSchedulerDbContext context)
        {
            if(!context.Appointments.Any())
            {
                context.AddRange(
                    new Appointment
                    {
                        Appointment_Name = "Bank",
                        Appointment_Description = "Deposit money",
                        CreatedDate= DateTime.Now,
                        StartDate= DateTime.Now,
                        EndDate= DateTime.Now.AddDays(25),
                        Location="Patra",
                        Reminder= ReminderIn.OneWeek,
                        PhoneNumber="6987976510",
                        Email="peternomik@gmail.com"
                    },
                    new Appointment
                    {
                        Appointment_Name = "Hospital",
                        Appointment_Description = "Give blood.",
                        CreatedDate = DateTime.Now.AddDays(2),
                        StartDate = DateTime.Now.AddDays(2),
                        EndDate = DateTime.Now.AddDays(6),
                        Location = "Patra",
                        Reminder = ReminderIn.TwelveHours,
                        PhoneNumber = "6987976510",
                        Email = "peternomik@gmail.com"
                    },
                    new Appointment
                    {
                        Appointment_Name = "Psychologist",
                        Appointment_Description = "Weekly Appointment",
                        CreatedDate = DateTime.Now.AddDays(6),
                        StartDate = DateTime.Now.AddDays(6),
                        EndDate = DateTime.Now.AddDays(12),
                        Location = "Patra",
                        Reminder = ReminderIn.TwelveHours,
                        PhoneNumber = "6987976510",
                        Email = "peternomik@gmail.com"
                    },
                    new Appointment
                    {
                        Appointment_Name = "University",
                        Appointment_Description = "Meet my professor",
                        CreatedDate = DateTime.Now.AddDays(12),
                        StartDate = DateTime.Now.AddDays(12),
                        EndDate = DateTime.Now.AddDays(15),
                        Location = "Patra",
                        Reminder = ReminderIn.TwelveHours,
                        PhoneNumber = "6987976510",
                        Email = "peternomik@gmail.com"
                    },
                    new Appointment
                    {
                        Appointment_Name = "Painter",
                        Appointment_Description = "Paint the House",
                        CreatedDate = DateTime.Now.AddDays(4),
                        StartDate = DateTime.Now.AddDays(4),
                        EndDate = DateTime.Now.AddDays(6),
                        Location = "Patra",
                        Reminder = ReminderIn.OneHour,
                        PhoneNumber = "6987976510",
                        Email = "peternomik@gmail.com"
                    },
                    new Appointment
                    {
                        Appointment_Name = "Dinner Reservations",
                        Appointment_Description = "Valentines Day",
                        CreatedDate = DateTime.Now.AddDays(2),
                        StartDate = DateTime.Now.AddDays(2),
                        EndDate = DateTime.Now.AddDays(8),
                        Location = "Patra",
                        Reminder = ReminderIn.TwoHours,
                        PhoneNumber = "6987976510",
                        Email = "peternomik@gmail.com"
                    }
                    ) ;
            }

            context.SaveChanges();
        }
    }
}
