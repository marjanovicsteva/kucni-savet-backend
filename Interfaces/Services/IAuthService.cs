using KucniSavetBackend.Domain;

namespace KucniSavetBackend.Interfaces.Services;

public interface IAuthService
{
    Task<User> LoginWithFacebookAsync(string accessToken, string householdName = "");
    string GenerateJwt(User user);
}