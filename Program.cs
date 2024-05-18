
using Appointments.Data;
using Appointments.DTOs;
using Appointments.Interfaces;
using Appointments.Repositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.Filters;
using System.Text.Json.Serialization;

// Gör så att man ser namn på kunder och företag istället för deras id
// underlätta hur man skriver in tider för möten i request body... eller from query?
// customers och companies ska inte gå att dubbelboka


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
            // En appointment består till del av en Customer
            // ... som i sin tur åter igen navigerar till appointments
            // det blir en loop/cycle som stoppas av följande kod
            builder.Services.AddControllers().AddJsonOptions(x => x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);

            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();

            // mestadels nytt
            builder.Services.AddSwaggerGen(options =>
            {
                options.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme
                {
                    In = ParameterLocation.Header,
                    Name = "Authorization",
                    Type = SecuritySchemeType.ApiKey
                });

                options.OperationFilter<SecurityRequirementsOperationFilter>();
            });

            builder.Services.AddDbContext<DataContext>(options => options.
            UseSqlServer(builder.Configuration.GetConnectionString("Connection")));

            // nytt
            builder.Services.AddAuthorization();

            // nytt
            builder.Services.AddIdentityApiEndpoints<IdentityUser>()
                .AddEntityFrameworkStores<DataContext>();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            // nytt
            app.MapIdentityApi<IdentityUser>();

            app.UseHttpsRedirection();

            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}
