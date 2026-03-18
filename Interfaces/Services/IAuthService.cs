using KucniSavetBackend.Domain;

namespace KucniSavetBackend.Interfaces.Services;

public interface IAuthService
{
    Task<User?> LoginWithFacebookAsync(string accessToken);
    string GenerateJwt(User user);
}