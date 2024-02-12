using Appointment_Scheduler.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.IO.Pipelines;

namespace Appointment_Scheduler.Data
{
    public class AppointmentSchedulerDbContext : IdentityDbContext
    {
        public AppointmentSchedulerDbContext(DbContextOptions<AppointmentSchedulerDbContext> options) : 
            base(options) { }
         public DbSet<Appointment> Appointments { get; set; }
    }
}
