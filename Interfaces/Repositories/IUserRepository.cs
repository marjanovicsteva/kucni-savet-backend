using KucniSavetBackend.Domain;

namespace KucniSavetBackend.Interfaces.Repositories;

public interface IUserRepository
{
    Task<User?> GetByIdAsync(string id);
    Task<User> CreateAsync(User user);
}