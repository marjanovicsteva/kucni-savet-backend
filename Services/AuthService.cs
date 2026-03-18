using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using KucniSavetBackend.Domain;
using KucniSavetBackend.DTO.Responses;
using KucniSavetBackend.Interfaces.Repositories;
using KucniSavetBackend.Interfaces.Services;
using Microsoft.IdentityModel.Tokens;

namespace KucniSavetBackend.Services;

public class AuthService : IAuthService
{
    private readonly IUserRepository _userRepository;
    private readonly IHouseholdRepository _householdRepository;
    private readonly IConfiguration _configuration;
    private readonly HttpClient _client;
    private readonly Uri FacebookUri = new("https://graph.facebook.com/v25.0");

    public AuthService(IUserRepository userRepository, IHouseholdRepository householdRepository, IConfiguration configuration)
    {
        _userRepository = userRepository;
        _householdRepository = householdRepository;
        _configuration = configuration;
        _client = new HttpClient
        {
            BaseAddress = FacebookUri
        };
    }

    public string GenerateJwt(User user)
    {
        var key = new SymmetricSecurityKey(
            System.Text.Encoding.UTF8.GetBytes(_configuration["Jwt:Secret"])
        );
        var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var claims = new[]
        {
            new Claim("userId", user.Id),
            new Claim("householdId", user.Household.Id)
        };

        var token = new JwtSecurityToken(
            issuer: _configuration["Jwt:Issuer"],
            audience: _configuration["Jwt:Audience"],
            claims: claims,
            expires: DateTime.UtcNow.AddDays(7),
            signingCredentials: credentials
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }

    public async Task<User?> LoginWithFacebookAsync(string accessToken)
    {
        if (accessToken == "")
            return null;

        FacebookMeResponse? facebookUser;
        try
        {
            facebookUser = await _client.GetFromJsonAsync<FacebookMeResponse>($"me?access_token={Uri.EscapeDataString(accessToken)}");
        } catch (Exception e)
        {
            throw new Exception("Invalid Facebook Token");
        }

        var user = await _userRepository.GetByFacebookIdAsync(facebookUser.Id);
        if (user is null)
        {
            // Create user
            var household = new Household
            {
                Name = $"{facebookUser.Name}'s Household"
            };
            household = await _householdRepository.CreateAsync(household);

            user = new User
            {
                FacebookId = facebookUser.Id,
                Name = facebookUser.Name,
                Household = household,
            };

            user = await _userRepository.CreateAsync(user);
        }

        return user;
    }
}