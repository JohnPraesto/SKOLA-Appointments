using AppointmentModels;
using Appointments.Data;
using Appointments.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Appointments.Repositories
{
    public class CustomerRepository : ICustomer
    {
        private readonly DataContext _context;

        public CustomerRepository(DataContext context)
        {
            _context = context;
        }
        public bool CustomerExists(int id)
        {
            return _context.Customers.Any(p => p.CustomerId == id);
        }

        public Customer CreateCustomer(string firstName, string lastName)
        {
            Customer newCustomer = new Customer()
            {
                FirstName = firstName,
                LastName = lastName
            };
            _context.Customers.Add(newCustomer);
            _context.SaveChanges();
            return newCustomer;
        }

        public bool DeleteCustomer(int id)
        {
            Customer customerToDelete = _context.Customers.FirstOrDefault(c => c.CustomerId == id); // Fanns ett where innan
            _context.Customers.Remove(customerToDelete);
            _context.SaveChanges();
            return true;
        }

        public ICollection<Customer> GetAllCustomers(string searchParameter)
        {
            if (string.IsNullOrWhiteSpace(searchParameter))
            {
                return _context.Customers.OrderBy(c => c.LastName).ToList();
            }
            else
            {
                return _context.Customers
                .Where(c => c.FirstName.Contains(searchParameter) || c.LastName.Contains(searchParameter))
                .OrderBy(c => c.LastName)
                .ToList();
            }
        }

        public Customer GetOneCustomer(int id)
        {
            //return _context.Customers.Where(c => c.CustomerId == id).FirstOrDefault();

            return _context.Customers
                   .Include(c => c.Appointments) // Eager loading appointments
                   .FirstOrDefault(c => c.CustomerId == id);
        }

        public bool UpdateCustomer(Customer customerToUpdate)
        {
            _context.Update(customerToUpdate);
            _context.SaveChanges();
            return true;
        }
    }
}
