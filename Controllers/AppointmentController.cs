using AppointmentModels;
using Appointments.DTOs;
using Appointments.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Appointments.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AppointmentController : ControllerBase
    {
        private readonly IAppointment _appointmentRepository;
        private readonly ICustomer _customerRepository;
        private readonly ICompany _companyRepository;
        private readonly IMapper _mapper;

        public AppointmentController(IAppointment appointmentRepository, ICustomer customerRepository, ICompany companyRepository, IMapper mapper)
        {
            _appointmentRepository = appointmentRepository;
            _customerRepository = customerRepository;
            _companyRepository = companyRepository;
            _mapper = mapper;
        }

        [HttpPost]
        public ActionResult CreateAppointment(AppointmentDTOForCreate newAppointment)
        {
            if (!_customerRepository.CustomerExists(newAppointment.CustomerId))
                return NotFound($"Customer ID {newAppointment.CustomerId} was not found in database");

            if (!_companyRepository.CompanyExists(newAppointment.CompanyId))
                return NotFound($"Company ID {newAppointment.CompanyId} was not found in database");

            _appointmentRepository.CreateAppointment(_mapper.Map<Appointment>(newAppointment));
            return Ok(newAppointment);
        }

        [HttpGet("Get all appointments")]
        public ActionResult GetAllAppointments()
        {
            return Ok(_mapper.Map<List<AppointmentDTO>>(_appointmentRepository.GetAllAppointments()));
        }

        [HttpGet("Get appointments by date")]
        public ActionResult GetAppointmentsByDate(DateTime from, DateTime to)
        {
            return Ok(_mapper.Map<List<AppointmentDTO>>(_appointmentRepository.GetAppointmentsByDate(from, to)));
        }

        [HttpGet("Get discarded appointments")]
        public ActionResult GetDiscarededAppointments()
        {
            return Ok(_appointmentRepository.GetDiscardedAppointments());
        }

        [HttpGet("Get one appointment")]
        public ActionResult GetAppointmentById(int id)
        {
            if (!_appointmentRepository.AppointmentExists(id))
                return NotFound($"Appointment with ID {id} was not found in database");

            return Ok(_appointmentRepository.GetAppointmentById(id));
        }

        [HttpDelete]
        public ActionResult DeleteAppointment(int id)
        {
            if (!_appointmentRepository.AppointmentExists(id))
                return NotFound($"Appointment with ID {id} was not found in database");

            _appointmentRepository.DeleteAppointment(id);

            return Ok($"Appointment with ID {id} successfully deleted");
        }

        [HttpPut]
        public ActionResult UpdateAppointment(int id, Appointment updatedAppointment)
        {
            if (id != updatedAppointment.AppointmentId)
                return BadRequest("Id from query and Id from request body does not match!");

            if (!_appointmentRepository.AppointmentExists(id))
                return NotFound($"Appointment with ID {id} was not found in database");

            Customer existingCustomer = _customerRepository.GetOneCustomer(updatedAppointment.CustomerId);
            Company existingCompany = _companyRepository.GetOneCompany(updatedAppointment.CompanyId);

            if (existingCustomer == null)
                return NotFound($"Customer ID {updatedAppointment.CustomerId} was not found in database");

            if (existingCompany == null)
                return NotFound($"Company ID {updatedAppointment.CompanyId} was not found in database");

            updatedAppointment.Customer = existingCustomer;
            updatedAppointment.Company = existingCompany;

            _appointmentRepository.UpdateAppointment(updatedAppointment);

            return Ok($"Appointment with ID {id} successfully updated");
        }
    }
}
