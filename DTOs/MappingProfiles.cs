using AppointmentModels;
using AutoMapper;

namespace Appointments.DTOs
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<Appointment, AppointmentDTO>().ReverseMap();
            CreateMap<Appointment, AppointmentDTOForCreate>().ReverseMap();
        }
    }
}
