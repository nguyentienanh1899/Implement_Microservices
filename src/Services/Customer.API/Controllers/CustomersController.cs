using Customer.API.Repositories.Interfaces;
using Customer.API.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Shared.DTOs.Customer;

namespace Customer.API.Controllers
{
    public static class CustomersController 
    {
        public static void MapCustomersAPI(this WebApplication app)
        {
            #region Minimal API .Net 6 CRUD
            app.MapGet("/", () => "Welcome to Customer API");

            app.MapGet("/api/customers", async (ICustomerService customerservice) => await customerservice.GetCustomersAsync());
            app.MapGet("/api/customers/{username}", async (string username, ICustomerService customerservice) =>
            {
                var customer = await customerservice.GetCustomerByUserNameAsync(username);
                return customer != null ? customer : Results.NotFound();
            });

            //app.MapPost("/api/customers", async (Customer.API.Entities.Customer customer, ICustomerRepository customerRepository) =>
            //{
            //    customerRepository.CreateAsync(customer);
            //    customerRepository.SaveChangesAsync();

            //    return Results.NoContent();
            //});

            //app.MapPut("/api/customers/{id}", async (int id, UpdateCustomerDto customerUpdate, ICustomerRepository customerRepository) =>
            //{
            //    var customerOld = await customerRepository.FindByCondition(x => x.Id.Equals(id)).SingleOrDefaultAsync();
            //    if (customerOld == null) return Results.NotFound();

            //    customerOld.UserName = customerUpdate.UserName;
            //    customerOld.FirstName = customerUpdate.FirstName;
            //    customerOld.LastName = customerUpdate.LastName;
            //    customerOld.EmailAddress = customerUpdate.EmailAddress;

            //    customerRepository.UpdateAsync(customerOld);
            //    customerRepository.SaveChangesAsync();

            //    return Results.NoContent();
            //});

            //app.MapDelete("/api/customers/{id}", async (int id, ICustomerRepository customerRepository) =>
            //{
            //    var customer = await customerRepository.FindByCondition(x => x.Id.Equals(id)).SingleOrDefaultAsync();
            //    if (customer == null) return Results.NotFound();

            //    await customerRepository.DeleteAsync(customer);
            //    await customerRepository.SaveChangesAsync();
            //    return Results.NoContent();
            //});
            #endregion
        }
    }
}
