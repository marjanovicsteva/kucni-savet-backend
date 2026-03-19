using KucniSavetBackend.Domain;

namespace KucniSavetBackend.Interfaces.Services;

public interface IAuthService
{
    Task<User?> LoginOrRegisterWithFacebookAsync(string accessToken);
    Task<User?> RegisterWithFacebookAndInviteCodeAsync(string accessToken, string inviteCode);
    string GenerateJwt(User user);
}