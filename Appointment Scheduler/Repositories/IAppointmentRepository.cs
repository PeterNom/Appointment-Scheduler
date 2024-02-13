using Appointment_Scheduler.Models;
using System.IO.Pipelines;

namespace Appointment_Scheduler.Repositories
{
    public interface IAppointmentRepository
    {
        Task<IEnumerable<Appointment>> GetAllAppointmentsAsync();
        Task<IEnumerable<Appointment>> GetDueAppointmentsAsync();
        Task<Appointment?> GetAppointmentByIdAsync(int appointId);
        Task<int> AddAppointmentsAsync(Appointment appointment);
        Task<int> UpdateAppointmentsAsync(Appointment appointment);
        Task<int> DeleteAppointmentsAsync(int appointId);
        Task<int> DeleteCompletedAppointmentsAsync();
        IEnumerable<Appointment> GetAllDueAppointmentsAsync();
        Task<int> GetAllAppointmentCountAsync();
        IEnumerable<Appointment> SearchAppointments(string searcQuery);
        
    }
}
