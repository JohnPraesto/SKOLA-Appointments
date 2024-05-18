using AppointmentModels;
using Appointments.Data;
using Appointments.Interfaces;

namespace Appointments.Repositories
{
    public class AppointmentRepository : IAppointment
    {
        private readonly DataContext _context;

        public AppointmentRepository(DataContext context)
        {
            _context = context;
        }
        public bool AppointmentExists(int id)
        {
            return _context.Appointments.Any(a => a.AppointmentId == id);
        }

        public Appointment CreateAppointment(Appointment newAppointment)
        {
            _context.Appointments.Add(newAppointment);
            _context.SaveChanges();
            return newAppointment;
        }

        public bool DeleteAppointment(int id)
        {
            Appointment appointmentToDelete = _context.Appointments.FirstOrDefault(c => c.AppointmentId == id);
            AppointmentHistory newHistory = new AppointmentHistory()
            {
                AppointmentId = appointmentToDelete.AppointmentId,
                CustomerId = appointmentToDelete.CustomerId,
                CompanyId = appointmentToDelete.CompanyId,
                AppointmentStart = appointmentToDelete.AppointmentStart,
                AppointmentEnd = appointmentToDelete.AppointmentEnd,
                Customer = appointmentToDelete.Customer,
                Company = appointmentToDelete.Company
            };
            _context.AppointmentHistories.Add(newHistory);
            _context.Appointments.Remove(appointmentToDelete);
            _context.SaveChanges();
            return true;
        }

        public ICollection<Appointment> GetAllAppointments()
        {
            return _context.Appointments.ToList();
        }

        public ICollection<AppointmentHistory> GetDiscardedAppointments()
        {
            return _context.AppointmentHistories.ToList();
        }

        public Appointment GetAppointmentById(int id)
        {
            return _context.Appointments.FirstOrDefault(a => a.AppointmentId == id);
        }
        public ICollection<Appointment> GetAppointmentsByDate(DateTime from, DateTime to)
        {
            return _context.Appointments.Where(a => a.AppointmentStart >= from && a.AppointmentEnd <= to).ToList();
        }

        public bool UpdateAppointment(Appointment appointmentToUpdate)
        {
            Appointment oldAppointment = _context.Appointments.FirstOrDefault(a => a.AppointmentId == appointmentToUpdate.AppointmentId);

            AppointmentHistory newHistory = new AppointmentHistory()
            {
                AppointmentId = oldAppointment.AppointmentId,
                CustomerId = oldAppointment.CustomerId,
                CompanyId = oldAppointment.CompanyId,
                AppointmentStart = oldAppointment.AppointmentStart,
                AppointmentEnd = oldAppointment.AppointmentEnd,
                Customer = oldAppointment.Customer,
                Company = oldAppointment.Company
            };
            _context.AppointmentHistories.Add(newHistory);
            _context.Appointments.Update(appointmentToUpdate);
            _context.SaveChanges();
            return true;
        }
    }
}
