using Microsoft.EntityFrameworkCore;

namespace Customer.API.Persistence
{
    public static class CustomerContextSeed
    {
        public static IHost SeedCustomerData(this IHost host)
        {
            using var scope = host.Services.CreateScope();
            var customerContext = scope.ServiceProvider.GetRequiredService<CustomerContext>();
            customerContext.Database.MigrateAsync().GetAwaiter().GetResult();

            var customer1 = new Entities.Customer()
            {
                UserName = "customer1",
                FirstName = "customer1",
                LastName = "customer1",
                EmailAddress = "customer1@gmail.com",
            };

            var customer2 = new Entities.Customer()
            {
                UserName = "customer2",
                FirstName = "customer2",
                LastName = "customer2",
                EmailAddress = "customer2@gmail.com",
            };
            CreateCustomer(customerContext, customer1).GetAwaiter().GetResult();
            CreateCustomer(customerContext, customer2).GetAwaiter().GetResult();

            return host;
        }

        public static async Task CreateCustomer(CustomerContext customerContext, Entities.Customer newCustomer)
        {
            var customer = await customerContext.Customers.SingleOrDefaultAsync(x=>x.UserName.Equals(newCustomer.UserName) || x.EmailAddress.Equals(newCustomer.EmailAddress));
            if (customer == null)
            {
                await customerContext.Customers.AddAsync(newCustomer);
                await customerContext.SaveChangesAsync();
            }
        }
    }
}
