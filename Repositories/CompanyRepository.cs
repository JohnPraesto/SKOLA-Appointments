using AppointmentModels;
using Appointments.Data;
using Appointments.Interfaces;

namespace Appointments.Repositories
{
    public class CompanyRepository : ICompany
    {
        private readonly DataContext _context;

        public CompanyRepository(DataContext context)
        {
            _context = context;
        }
        public bool CompanyExists(int id)
        {
            return _context.Companies.Any(p => p.CompanyId == id);
        }

        public Company CreateCompany(string name)
        {
            Company newCompany = new Company()
            {
                Name = name
            };
            _context.Companies.Add(newCompany);
            _context.SaveChanges();
            return newCompany;
        }

        public bool DeleteCompany(int id)
        {
            Company companyToDelete = _context.Companies.FirstOrDefault(c => c.CompanyId == id);
            _context.Companies.Remove(companyToDelete);
            _context.SaveChanges();
            return true;
        }

        public ICollection<Company> GetAllCompanies()
        {
            return _context.Companies.ToList();
        }

        public Company GetOneCompany(int id)
        {
            return _context.Companies.Where(c => c.CompanyId == id).FirstOrDefault();
        }

        public bool UpdateCompany(Company companyToUpdate)
        {
            _context.Update(companyToUpdate);
            _context.SaveChanges();
            return true;
        }
    }
}
