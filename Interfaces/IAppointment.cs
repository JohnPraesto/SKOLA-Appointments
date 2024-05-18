using AppointmentModels;

namespace Appointments.Interfaces
{
    public interface IAppointment
    {
        ICollection<Appointment> GetAllAppointments();
        ICollection<AppointmentHistory> GetDiscardedAppointments();
        Appointment GetAppointmentById(int id);
        ICollection<Appointment> GetAppointmentsByDate(DateTime from, DateTime to);
        Appointment CreateAppointment(Appointment newAppointment); // behövs inte själva objekten?
        bool UpdateAppointment(Appointment appointmentToUpdate);
        bool DeleteAppointment(int id);
        bool AppointmentExists(int id);
    }
}
