
using Appointments.Data;
using Appointments.DTOs;
using Appointments.Interfaces;
using Appointments.Repositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;

// G�r s� att man ser namn p� kunder och f�retag ist�llet f�r deras id
// underl�tta hur man skriver in tider f�r m�ten i request body... eller from query?
// customers och companies ska inte g� att dubbelboka


namespace Appointments
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();

            builder.Services.AddAutoMapper(typeof(MappingProfiles));

            builder.Services.AddScoped<ICustomer, CustomerRepository>();
            builder.Services.AddScoped<ICompany, CompanyRepository>();
            builder.Services.AddScoped<IAppointment, AppointmentRepository>();

            // Customer klassen har en navigation property till Appointments
            // En appointment best�r till del av en Customer
            // ... som i sin tur �ter igen navigerar till appointments
            // det blir en loop/cycle som stoppas av f�ljande kod
            builder.Services.AddControllers().AddJsonOptions(x => x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);

            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Services.AddDbContext<DataContext>(options => options.
            UseSqlServer(builder.Configuration.GetConnectionString("Connection")));

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}
