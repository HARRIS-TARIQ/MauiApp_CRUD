using MauiApp2.Models;

namespace MauiApp2.Repositories.Interfaces
{
    public interface IUserRepository
    {
        Task<User?> GetByEmailAsync(string email);
        Task<User?> ValidateUserAsync(string email, string password);
        Task<User> AddAsync(User user);
    }
}
