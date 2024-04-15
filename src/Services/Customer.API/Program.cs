using Common.Logging;
using Contracts.Common.Interfaces;
using Customer.API.Persistence;
using Customer.API.Repositories;
using Customer.API.Repositories.Interfaces;
using Customer.API.Services;
using Customer.API.Services.Interfaces;
using Infrastructure.Common;
using Microsoft.EntityFrameworkCore;
using Serilog;
using Shared.DTOs.Customer;
using System.Net.WebSockets;

var builder = WebApplication.CreateBuilder(args);
builder.Host.UseSerilog(Serilogger.Configure);

Log.Information("Start Customer API up");

try
{
    // Add services to the container.

    builder.Services.AddControllers();
    // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen();

    var connectionString = builder.Configuration.GetConnectionString("DefaultConnectionString");
    builder.Services.AddDbContext<CustomerContext>(options => options.UseNpgsql(connectionString));

    builder.Services.AddScoped<ICustomerRepository, CustomerRepository>()
        .AddScoped(typeof(IRepositoryBaseAsync<,,>), typeof(RepositoryBaseAsync<,,>))
        .AddScoped(typeof(IUnitOfWork<>), typeof(UnitOfWork<>))
        .AddScoped<ICustomerService, CustomerService>();

    var app = builder.Build();

    #region Minimal API .Net 6
    // Start Minimal API .Net 6
    app.MapGet("/", () => "Welcome to Customer API");

    app.MapGet("/api/customers", async (ICustomerService customerservice) => await customerservice.GetCustomersAsync());
    app.MapGet("/api/customers/{username}", async (string username, ICustomerService customerservice) =>
    {
        var customer = await customerservice.GetCustomerByUserNameAsync(username);
        return customer != null ? Results.Ok(customer) : Results.NotFound();
    });

    app.MapPost("/api/customers", async (Customer.API.Entities.Customer customer, ICustomerRepository customerRepository) =>
    {
        customerRepository.CreateAsync(customer);
        customerRepository.SaveChangesAsync();

        return Results.NoContent();
    });

    app.MapPut("/api/customers/{id}", async (int id, UpdateCustomerDto customerUpdate, ICustomerRepository customerRepository) =>
    {
        var customerOld = await customerRepository.FindByCondition(x => x.Id.Equals(id)).SingleOrDefaultAsync();
        if (customerOld == null) return Results.NotFound();

        customerOld.UserName = customerUpdate.UserName;
        customerOld.FirstName = customerUpdate.FirstName;
        customerOld.LastName = customerUpdate.LastName;
        customerOld.EmailAddress = customerUpdate.EmailAddress;

        customerRepository.UpdateAsync(customerOld);
        customerRepository.SaveChangesAsync();

        return Results.NoContent();
    });

    app.MapDelete("/api/customers/{id}", async (int id, ICustomerRepository customerRepository) =>
    {
        var customer = await customerRepository.FindByCondition(x=>x.Id.Equals(id)).SingleOrDefaultAsync();
        if(customer == null) return Results.NotFound();

        await customerRepository.DeleteAsync(customer);
        await customerRepository.SaveChangesAsync();
        return Results.NoContent();
    });
    #endregion

    // Configure the HTTP request pipeline.
    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI();
    }

    app.UseHttpsRedirection();

    app.UseAuthorization();

    app.MapControllers();

    app.SeedCustomerData().Run();
}
catch (Exception ex)
{
    // handeler exception when migration
    string type = ex.GetType().Name;
    if (type.Equals("StopTheHostException", StringComparison.Ordinal)) throw;

    Log.Fatal(ex, "Unhandled exception");
}
finally
{
    Log.Information("Shut down Customer API complete");
    Log.CloseAndFlush();
}


