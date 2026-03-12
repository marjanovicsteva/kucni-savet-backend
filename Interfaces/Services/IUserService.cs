using KucniSavetBackend.DTO.Requests.User;
using KucniSavetBackend.DTO.Responses;

namespace KucniSavetBackend.Interfaces.Services;

public interface IUserService
{
    Task<UserResponse> GetByIdAsync(string id);
    Task<UserResponse> CreateAsync(CreateUserRequest request);
    Task<UserResponse> CreateWithHousehold(CreateUserRequest request);
}