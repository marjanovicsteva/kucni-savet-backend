using KucniSavetBackend.DTO.Requests.User;
using KucniSavetBackend.DTO.Responses;

namespace KucniSavetBackend.Interfaces.Services;

public interface IUserService
{
    Task<UserResponse?> GetByIdAsync(string id);
    Task<UserResponse?> GetByFacebookIdAsync(string facebookId);
    Task<FacebookMeResponse?> GetUserFacebookDataAsync(string facebookAccessToken);
    Task<UserResponse?> CreateAsync(CreateUserRequest request);
}