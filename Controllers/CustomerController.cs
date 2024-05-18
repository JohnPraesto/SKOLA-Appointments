using AppointmentModels;
using Appointments.Interfaces;
using Appointments.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Appointments.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly ICustomer _customerRepository;

        public CustomerController(ICustomer customerRepository)
        {
            _customerRepository = customerRepository;
        }

        [HttpPost]
        public ActionResult<Customer> CreateCustomer(string firstName, string lastName) // Vad händer utan <Customer>?
        {
            var newCustomer = _customerRepository.CreateCustomer(firstName, lastName);
            return Ok(newCustomer);
        }

        [HttpGet("Get all customers")]
        public ActionResult GetAllCustomers(string searchParameter)
        {
            var customers = _customerRepository.GetAllCustomers(searchParameter);
            return Ok(customers);
        }

        [HttpGet("Get one customer")]
        public ActionResult GetOneCustomer(int id)
        {
            if (!_customerRepository.CustomerExists(id))
                return NotFound($"Customer with ID {id} was not found in database");

            return Ok(_customerRepository.GetOneCustomer(id));
        }

        [HttpDelete]
        public ActionResult DeleteCustomer(int id)
        {
            if (!_customerRepository.CustomerExists(id))
                return NotFound($"Customer with ID {id} was not found in database");

            _customerRepository.DeleteCustomer(id);

            return Ok($"Customer with ID {id} successfully deleted");
        }

        [HttpPut]
        public ActionResult UpdateCustomer(int id, Customer updatedCustomer)
        {
            if (id != updatedCustomer.CustomerId)
                return BadRequest("Id from query and Id from request body does not match!");

            if (!_customerRepository.CustomerExists(id))
                return NotFound($"Customer with ID {id} was not found in database");

            _customerRepository.UpdateCustomer(updatedCustomer);

            return Ok($"Customer with ID {id} successfully updated");
        }
    }
}
