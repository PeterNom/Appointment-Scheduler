using Appointment_Scheduler.Models;
using System.IO.Pipelines;

namespace Appointment_Scheduler.Repositories
{
    public interface IAppointmentRepository
    {
        Task<IEnumerable<Appointment>> GetAllAppointmentsAsync();
        Task<Appointment?> GetAppointmentByIdAsync(int appointId);
        Task<int> AddAppointmentsAsync(Appointment appointment);
        Task<int> UpdateAppointmentsAsync(Appointment appointment);
        Task<int> DeleteAppointmentsAsync(Appointment appointment);
        Task<int> GetAllAppointmentCountAsync(Appointment appointment);
        Task<IEnumerable<Appointment>> SearchAppointments(DateTime startDate, DateTime endDate);
    }
}
