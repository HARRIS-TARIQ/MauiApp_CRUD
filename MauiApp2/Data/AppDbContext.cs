using Microsoft.EntityFrameworkCore;
using MauiApp2.Models;

namespace MauiApp2.Data
{
    public class AppDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Product> Products { get; set; }

        private readonly string _dbPath;

        public AppDbContext()
        {
            _dbPath = Path.Combine(FileSystem.AppDataDirectory, "mauicrudapp.db3");
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite($"Filename={_dbPath}");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().HasIndex(u => u.Email).IsUnique();
            base.OnModelCreating(modelBuilder);
        }
    }
}
