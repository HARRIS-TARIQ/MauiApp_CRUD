using MauiApp2.Data;
using MauiApp2.Models;
using MauiApp2.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace MauiApp2.Repositories.Implementation
{
    public class ProductRepository : IProductRepository
    {
        private readonly AppDbContext _context;
        public ProductRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<Product>> GetAllAsync()
        {
            return await _context.Products
                .OrderByDescending(p => p.CreatedDate)
                .ToListAsync();
        }

        public async Task<Product?> GetByIdAsync(int id)
        {
            return await _context.Products.FindAsync(id);
        }

        public async Task<Product> AddAsync(Product product)
        {
            product.CreatedDate = DateTime.Now;
            product.UpdatedDate = DateTime.Now;
            _context.Products.Add(product);
            await _context.SaveChangesAsync();
            return product;
        }

        public async Task<Product> UpdateAsync(Product product)
        {
            product.UpdatedDate = DateTime.Now;
            _context.Products.Update(product);
            await _context.SaveChangesAsync();
            return product;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var product = await _context.Products.FindAsync(id);
            if (product == null) return false;

            _context.Products.Remove(product);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<List<Product>> SearchAsync(string keyword)
        {
            if (string.IsNullOrWhiteSpace(keyword))
                return await GetAllAsync();

            return await _context.Products
                .Where(p => p.Name.Contains(keyword) || p.Category.Contains(keyword))
                .ToListAsync();
        }
    }
}
