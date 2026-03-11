using KucniSavetBackend.Domain;
using KucniSavetBackend.DTO.Requests.User;
using KucniSavetBackend.DTO.Responses;
using KucniSavetBackend.Interfaces.Repositories;
using KucniSavetBackend.Interfaces.Services;
using KucniSavetBackend.Mappers;

namespace KucniSavetBackend.Services;

public class UserService : IUserService
{
    private readonly IUserRepository _userRepository;
    private readonly IHouseholdRepository _householdRepository;

    public UserService(IUserRepository userRepository, IHouseholdRepository householdRepository)
    {
        _userRepository = userRepository;
        _householdRepository = householdRepository;
    }

    public async Task<UserResponse> CreateAsync(CreateUserRequest request)
    {
        // Do validation at the beginning

        var user = new User
        {
            Name = request.Name,
            Email = request.Email,
            Image = request.Image
        };

        user = await _userRepository.CreateAsync(user);

        return UserMapper.ToResponse(user);
    }

    public async Task<UserResponse> CreateWithHousehold(CreateUserRequest request)
    {
        var household = new Household
        {
            Name = request.HouseholdName
        };

        household = await _householdRepository.CreateAsync(household);

        var user = new User
        {
            Name = request.Name,
            Email = request.Email,
            Image = request.Image,
            Household = household
        };

        user = await _userRepository.CreateAsync(user);

        return UserMapper.ToResponse(user);
    }

    public async Task<UserResponse?> GetByIdAsync(string id)
    {
        var user = await _userRepository.GetByIdAsync(id);
        return user is not null ? UserMapper.ToResponse(user) : null;
    }
}