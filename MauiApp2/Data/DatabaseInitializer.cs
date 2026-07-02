using MauiApp2.Models;

namespace MauiApp2.Data
{
    public class DatabaseInitializer
    {
        private readonly AppDbContext _context;

        public DatabaseInitializer(AppDbContext context)
        {
            _context = context;
        }

        public async Task InitializeAsync()
        {
            await _context.Database.EnsureCreatedAsync();

            if (!_context.Users.Any())
            {
                _context.Users.Add(new User
                {
                    Email = "admin@devzologics.com",
                    Password = "Admin@123",
                    FullName = "Administrator"
                });
                await _context.SaveChangesAsync();
            }

            if (!_context.Products.Any())
            {
                _context.Products.AddRange(
                    new Product { Name = "Wireless Mouse", Description = "Ergonomic wireless mouse", Price = 15.99m, Quantity = 50, Category = "Electronics" },
                    new Product { Name = "Mechanical Keyboard", Description = "RGB mechanical keyboard", Price = 45.50m, Quantity = 30, Category = "Electronics" },
                    new Product { Name = "Office Chair", Description = "Comfortable ergonomic chair", Price = 120.00m, Quantity = 15, Category = "Furniture" }
                );
                await _context.SaveChangesAsync();
            }
        }
    }
}
