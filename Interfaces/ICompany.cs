using AppointmentModels;

namespace Appointments.Interfaces
{
    public interface ICompany
    {
        ICollection<Company> GetAllCompanies();
        Company GetOneCompany(int id);
        Company CreateCompany(string name);
        bool UpdateCompany(Company companyToUpdate);
        bool DeleteCompany(int id);
        bool CompanyExists(int id);
    }
}
