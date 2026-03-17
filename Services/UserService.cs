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
    private readonly Uri FacebookUri = new("https://graph.facebook.com/v25.0");

    public UserService(IUserRepository userRepository, IHouseholdRepository householdRepository)
    {
        _userRepository = userRepository;
        _householdRepository = householdRepository;
    }

    public async Task<UserResponse?> CreateAsync(CreateUserRequest request)
    {
        // Do validation at the beginning
        var household = new Household
        {
            Name = request.HouseholdName
        };
        household = await _householdRepository.CreateAsync(household);

        var facebookData = await GetUserFacebookDataAsync(request.FacebookAccessToken);
        var user = new User
        {
            FacebookId = facebookData.Id,
            Name = facebookData.Name,
            Household = household,
        };
        
        user = await _userRepository.CreateAsync(user);

        return UserMapper.ToResponse(user);
    }

    public Task<UserResponse?> GetByFacebookIdAsync(string facebookId)
    {
        throw new NotImplementedException();
    }

    public async Task<UserResponse?> GetByIdAsync(string id)
    {
        var user = await _userRepository.GetByIdAsync(id);
        return user is not null ? UserMapper.ToResponse(user) : null;
    }

    public async Task<FacebookMeResponse?> GetUserFacebookDataAsync(string facebookAccessToken)
    {
        var client = new HttpClient
        {
            BaseAddress = FacebookUri
        };

        using HttpResponseMessage response = await client.GetAsync($"me?access_token={facebookAccessToken}");

        response.EnsureSuccessStatusCode();

        var jsonResponse = await response.Content.ReadFromJsonAsync<FacebookMeResponse>();

        return jsonResponse ?? null;
    }
}