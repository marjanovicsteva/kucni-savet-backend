using KucniSavetBackend.Domain;
using KucniSavetBackend.DTO.Requests.User;
using KucniSavetBackend.DTO.Responses;
using KucniSavetBackend.Interfaces.Repositories;
using KucniSavetBackend.Interfaces.Services;
using KucniSavetBackend.Mappers;

namespace KucniSavetBackend.Services;

public class UserService(IUserRepository userRepository, IHouseholdRepository householdRepository, HttpClient client) : IUserService
{
    private static readonly string DiceBearUri = "https://api.dicebear.com/9.x/croodles/jpg";
    private readonly IUserRepository _userRepository = userRepository;
    private readonly IHouseholdRepository _householdRepository = householdRepository;
    private readonly HttpClient _client = client;

    public async Task<User?> CreateAsync(User? user)
    {
        // Do validation at the beginning
        if (user is null)
            return null;
        
        user = await _userRepository.CreateAsync(user);

        return user;
    }

    public async Task<User?> CreateWithInviteCodeAsync(string name, string householdId)
    {
        var household = await _householdRepository.GetByIdAsync(householdId, true);

        if (household is null)
            return null;

        byte[] imageBytes = await _client.GetByteArrayAsync($"{DiceBearUri}?size=256&seed={name}");
        string imageString = Convert.ToBase64String(imageBytes);

        var user = new User
        {
            Name = name,
            Household = household,
            Image = $"data:image/jpeg;base64,{imageString}",
            InviteCode = Guid.NewGuid().ToString()
        };

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