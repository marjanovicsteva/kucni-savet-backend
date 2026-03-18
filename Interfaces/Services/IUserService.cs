using KucniSavetBackend.Domain;
using KucniSavetBackend.DTO.Responses;

namespace KucniSavetBackend.Interfaces.Services;

public interface IUserService
{
    Task<User?> GetByIdAsync(string id);
    Task<User?> GetByFacebookIdAsync(string facebookId);
    Task<User?> CreateAsync(User user);
}