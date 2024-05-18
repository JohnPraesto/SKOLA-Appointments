using Microsoft.EntityFrameworkCore;
using AppointmentModels;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace Appointments.Data
{
    public class DataContext : IdentityDbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
        }

        public DbSet<Customer> Customers { get; set; }
        public DbSet<Appointment> Appointments { get; set; }
        public DbSet<AppointmentHistory> AppointmentHistories { get; set; }
        public DbSet<Company> Companies { get; set; }
    }
}
