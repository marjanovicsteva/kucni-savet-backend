using KucniSavetBackend.Domain;

namespace KucniSavetBackend.Interfaces.Repositories;

public interface IUserRepository
{
    Task<User?> GetByIdAsync(string key, bool prefixed = false);
    Task<User?> GetByFacebookIdAsync(string facebookId);
    Task<User?> CreateAsync(User user);
    Task<User?> UpdateAsync(User user);
    Task DeleteAsync(string key, bool prefixed = false);
    Task DeleteAsync(User user);
}