using Appointment_Scheduler.Models;
using System.IO.Pipelines;

namespace Appointment_Scheduler.Repositories
{
    public interface IAppointmentRepository
    {
        IEnumerable<Appointment> AllAppointments { get; }
        Appointment? GetAppointmentById(int pieId);
        IEnumerable<Appointment> SearchAppointments(DateTime startDate, DateTime endDate);
    }
}
