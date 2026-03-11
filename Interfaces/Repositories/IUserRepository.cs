using KucniSavetBackend.Domain;

namespace KucniSavetBackend.Interfaces.Repositories;

public interface IUserRepository
{
    Task<User?> GetByIdAsync(string key, bool prefixed = false);
    Task<User?> CreateAsync(User user);
}