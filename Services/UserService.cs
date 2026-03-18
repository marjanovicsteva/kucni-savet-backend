using KucniSavetBackend.Domain;
using KucniSavetBackend.DTO.Requests.User;
using KucniSavetBackend.DTO.Responses;
using KucniSavetBackend.Interfaces.Repositories;
using KucniSavetBackend.Interfaces.Services;
using KucniSavetBackend.Mappers;

namespace KucniSavetBackend.Services;

public class UserService(IUserRepository userRepository, IHouseholdRepository householdRepository) : IUserService
{
    private readonly IUserRepository _userRepository = userRepository;
    private readonly IHouseholdRepository _householdRepository = householdRepository;
    private readonly Uri FacebookUri = new("https://graph.facebook.com/v25.0");

    public async Task<User?> CreateAsync(User user)
    {
        // Do validation at the beginning
        
        user = await _userRepository.CreateAsync(user);

        return user;
    }

    public async Task<User?> GetByFacebookIdAsync(string facebookId)
    {
        var user = await _userRepository.GetByFacebookIdAsync(facebookId);
        return user;
    }

    public async Task<User?> GetByIdAsync(string id)
    {
        var user = await _userRepository.GetByIdAsync(id);
        return user;
    }
}