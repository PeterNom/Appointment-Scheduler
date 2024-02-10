using Appointment_Scheduler.Data;
using Appointment_Scheduler.Models;

namespace Appointment_Scheduler.Repositories
{
    public class AppointmentRepository : IAppointmentRepository
    {
        private readonly AppointmentSchedulerDbContext _SchedulerDbContext;

        public AppointmentRepository(AppointmentSchedulerDbContext schedulerDbContext)
        {
            _SchedulerDbContext = schedulerDbContext;
        }

        public IEnumerable<Appointment> AllAppointments
        {
            get
            {
                return _SchedulerDbContext.Appointments.ToList();
            }   
        }

        public Appointment? GetAppointmentById(int pieId)
        {
            return _SchedulerDbContext.Appointments.FirstOrDefault(p => p.AppointmentId == pieId);
        }

        public IEnumerable<Appointment> SearchAppointments(DateTime startDate, DateTime endDate)
        {

            throw new NotImplementedException();
        }
    }
}
