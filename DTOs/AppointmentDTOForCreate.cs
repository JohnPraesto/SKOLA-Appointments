namespace Appointments.DTOs
{
    public class AppointmentDTOForCreate
    {
        public int CustomerId { get; set; }
        public int CompanyId { get; set; }
        public DateTime AppointmentStart { get; set; }
        public DateTime AppointmentEnd { get; set; }
    }
}
