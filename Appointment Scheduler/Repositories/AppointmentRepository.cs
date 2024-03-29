﻿using Appointment_Scheduler.Data;
using Appointment_Scheduler.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace Appointment_Scheduler.Repositories
{
    public class AppointmentRepository : IAppointmentRepository
    {
        private readonly AppointmentSchedulerDbContext _SchedulerDbContext;

        public AppointmentRepository(AppointmentSchedulerDbContext schedulerDbContext)
        {
            _SchedulerDbContext = schedulerDbContext;
        }

        public  async Task<IEnumerable<Appointment>> GetAllAppointmentsAsync()
        {
            
            return await _SchedulerDbContext.Appointments
                .OrderBy(app=>app.EndDate)
                .AsNoTracking()
                .ToListAsync();  
        }

        public async Task<IEnumerable<Appointment>> GetDueAppointmentsAsync()
        {
            return await _SchedulerDbContext.Appointments
                .Where(app => app.EndDate > DateTime.Now && app.EndDate < DateTime.Now.AddDays(7))
                .OrderBy(app => app.EndDate)
                .AsNoTracking()
                .Take(3)
                .ToListAsync();
        }

        public async Task<Appointment?> GetAppointmentByIdAsync(int pieId)
        {
            return await _SchedulerDbContext.Appointments.AsNoTracking().FirstOrDefaultAsync(p => p.AppointmentId == pieId);
        }

        public async Task<int> AddAppointmentsAsync(Appointment appointment)
        {
            await _SchedulerDbContext.Appointments.AddAsync(appointment);

            return await _SchedulerDbContext.SaveChangesAsync();
        }

        public async Task<int> UpdateAppointmentsAsync(Appointment appointment)
        {
            var appointmentToUpdate = await _SchedulerDbContext.Appointments.FirstOrDefaultAsync(app=>app.AppointmentId == appointment.AppointmentId);

            if (appointmentToUpdate != null)
            {
                appointmentToUpdate.AppointmentId = appointment.AppointmentId;
                appointmentToUpdate.Appointment_Name = appointment.Appointment_Name;
                appointmentToUpdate.Appointment_Description = appointment.Appointment_Description;

                appointmentToUpdate.Location = appointment.Location;
                
                appointmentToUpdate.StartDate = appointment.StartDate;
                appointmentToUpdate.CreatedDate = appointment.CreatedDate;
                appointmentToUpdate.EndDate = appointment.EndDate;

                appointmentToUpdate.Reminder = appointment.Reminder;

                appointmentToUpdate.Email = appointment.Email;
                appointmentToUpdate.PhoneNumber = appointment.PhoneNumber;


                _SchedulerDbContext.Appointments.Update(appointmentToUpdate);
                return await _SchedulerDbContext.SaveChangesAsync();
            }
            else
            {
                throw new ArgumentException($"The appointment you try to update doesn't seem to exist.");
            }
        }

        public async Task<int> DeleteAppointmentsAsync(int id)
        {
            var appointmentToDelete = await _SchedulerDbContext.Appointments.FirstOrDefaultAsync(app=>app.AppointmentId == id);

            if (appointmentToDelete != null)
            {
                _SchedulerDbContext.Appointments.Remove(appointmentToDelete);
                return await _SchedulerDbContext.SaveChangesAsync();
            }
            else
            {
                throw new ArgumentException($"The appointment to delete can't be found.");
            }
        }

        public async Task<int> DeleteCompletedAppointmentsAsync()
        {
            var appointmentToDelete = GetAllDueAppointmentsAsync();

            if (appointmentToDelete != null)
            {
                _SchedulerDbContext.Appointments.RemoveRange(appointmentToDelete);
                return await _SchedulerDbContext.SaveChangesAsync();
            }
            else
            {
                throw new ArgumentException($"There are no appointments to delete.");
            }
        }

        public IEnumerable<Appointment> GetAllDueAppointmentsAsync()
        {
            var appointmentDue = _SchedulerDbContext.Appointments.Where(app => app.EndDate < DateTime.Now).ToList();

            return appointmentDue;
        }
        
        public async Task<int> GetAllAppointmentCountAsync()
        {
            return await _SchedulerDbContext.Appointments.CountAsync();
        }

        public IEnumerable<Appointment> SearchAppointments(string searchQuery)
        {
            var result = _SchedulerDbContext.Appointments.Where(app=> app.Appointment_Name.Contains(searchQuery));

            return result;
        }
    }
}
