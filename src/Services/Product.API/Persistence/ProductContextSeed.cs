using Product.API.Entities;
using ILogger = Serilog.ILogger;
namespace Product.API.Persistence
{
    public class ProductContextSeed
    {
        public static async Task SeedProductAsync(ProductContext productContext, ILogger logger)
        {
            if (!productContext.Products.Any())
            {
                productContext.AddRange(getCatalogProducts());
                await productContext.SaveChangesAsync();
                logger.Information("Seeded data for Product DB associated with context {DbContextName}", nameof(ProductContextSeed));
            }
        }

        private static IEnumerable<CatalogProduct> getCatalogProducts()
        {
            return new List<CatalogProduct>
            {
                new()
                {
                    No = "Iphone",
                    Name = "Iphone15",
                    Summary = "IPhone 15: pinnacle of Apple tech, advanced camera, blazing A16 chip, stunning display.",
                    Description = "Sleek design, powerful A16 chip, AI-driven camera, vibrant OLED display. Experience innovation with iPhone 15.",
                    Price = (decimal)3000.50
                },
                new()
                {
                    No = "Iphone",
                    Name = "Iphone11",
                    Summary = "IPhone 11: pinnacle of Apple tech, advanced camera, blazing A16 chip, stunning display.",
                    Description = "Sleek design, powerful A16 chip, AI-driven camera, vibrant OLED display. Experience innovation with iPhone 15.",
                    Price = (decimal)2000.50
                }
            };
        }
    }
}
