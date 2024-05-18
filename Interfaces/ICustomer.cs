using AppointmentModels;

namespace Appointments.Interfaces
{
    public interface ICustomer // Prova att göra en ICommons<T> - ett interface som tillhandahåller metoder som används av flera repositorys
    {
        ICollection<Customer> GetAllCustomers(string searchParameter);
        Customer GetOneCustomer(int id);
        Customer CreateCustomer(string firstName, string lastName);
        bool UpdateCustomer(Customer customerToUpdate);
        bool DeleteCustomer(int id);
        bool CustomerExists(int id);
    }
}
