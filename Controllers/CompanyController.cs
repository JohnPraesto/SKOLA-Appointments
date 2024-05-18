using AppointmentModels;
using Appointments.Interfaces;
using Appointments.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Appointments.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CompanyController : ControllerBase
    {
        private readonly ICompany _companyRepository;

        public CompanyController(ICompany companyRepository)
        {
            _companyRepository = companyRepository;
        }

        [HttpPost]
        public ActionResult<Company> CreateCompany(string name)
        {
            var newCompany = _companyRepository.CreateCompany(name);
            return Ok(newCompany);
        }

        [HttpGet("Get all companies")]
        public ActionResult GetAllCompanies()
        {
            return Ok(_companyRepository.GetAllCompanies());
        }

        [HttpGet("Get one company")]
        public ActionResult GetOneCompany(int id)
        {
            if (!_companyRepository.CompanyExists(id))
                return NotFound($"Company with ID {id} was not found in database");

            return Ok(_companyRepository.GetOneCompany(id));
        }

        [HttpDelete]
        public ActionResult DeleteCompany(int id)
        {
            if (!_companyRepository.CompanyExists(id))
                return NotFound($"Company with ID {id} was not found in database");

            _companyRepository.DeleteCompany(id);

            return Ok($"Company with ID {id} successfully deleted");
        }

        [HttpPut]
        public ActionResult UpdateCompany(int id, Company updatedCompany)
        {
            if (id != updatedCompany.CompanyId)
                return BadRequest("Id from query and Id from request body does not match!");

            if (!_companyRepository.CompanyExists(id))
                return NotFound($"Company with ID {id} was not found in database");

            _companyRepository.UpdateCompany(updatedCompany);

            return Ok($"Company with ID {id} successfully updated");
        }
    }
}
