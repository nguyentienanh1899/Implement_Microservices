using Microsoft.EntityFrameworkCore;
using Ordering.Infrastructure.Persistence;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ordering.Infrastructure
{
    public class OrderContextSeed
    {
        private readonly ILogger _logger;
        private readonly OrderContext _context;

        public OrderContextSeed(ILogger logger, OrderContext context)
        {
            _logger = logger;
            _context = context;
        }

        public async Task InitialiseAsync()
        {
            try
            {
                if (_context.Database.IsSqlServer()) { await _context.Database.MigrateAsync(); }
            }
            catch (Exception e)
            {
                _logger.Error(e, "An error occurred while initialising the database");
                throw;
            }
        }

        public async Task SeedAsync()
        {
            try
            {
                await TrySeedAsync();
                await _context.SaveChangesAsync();
            }
            catch (Exception e)
            {
                _logger.Error(e, "An error occurred while initialising the database");
                throw;
            }
        }

        public async Task TrySeedAsync()
        {
            if (!_context.Orders.Any())
            {
                await _context.Orders.AddRangeAsync(
                    new Domain.Entities.Order
                    {
                        UserName = "customer1",
                        FirstName = "customer1",
                        LastName = "customer1",
                        EmailAddress = "customer1@gmail.com",
                        ShippingAddress = "So 8 Phan Chu Trinh",
                        InvoiceAddress = "Viet Nam",
                        TotalPrice = 800
                    });
            }
        }
    }
}
