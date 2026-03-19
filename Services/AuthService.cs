using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using KucniSavetBackend.Domain;
using KucniSavetBackend.DTO.Responses;
using KucniSavetBackend.Exceptions;
using KucniSavetBackend.Interfaces.Repositories;
using KucniSavetBackend.Interfaces.Services;
using KucniSavetBackend.Mappers;
using Microsoft.IdentityModel.Tokens;

namespace KucniSavetBackend.Services;

public class AuthService(IUserRepository userRepository, IHouseholdRepository householdRepository, IConfiguration configuration, HttpClient client) : IAuthService
{
    private static readonly string FacebookUri = "https://graph.facebook.com/v25.0";
    private static readonly string DiceBearUri = "https://api.dicebear.com/9.x/croodles/jpg";
    private readonly IUserRepository _userRepository = userRepository;
    private readonly IHouseholdRepository _householdRepository = householdRepository;
    private readonly IConfiguration _configuration = configuration;
    private readonly HttpClient _client = client;
    
    public string GenerateJwt(User user)
    {
        if (user is null)
            throw new OperationalException("Invalid user");

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

    public async Task<User?> LoginOrRegisterWithFacebookAsync(string accessToken)
    {
        if (accessToken == "")
            return null;

        FacebookMeResponse? facebookUser;
        try
        {
            facebookUser = await _client.GetFromJsonAsync<FacebookMeResponse>($"{FacebookUri}/me?access_token={Uri.EscapeDataString(accessToken)}");

            if (facebookUser is null)
                return null;
        } catch
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

            byte[] imageBytes = await _client.GetByteArrayAsync($"{DiceBearUri}?size=512&seed={facebookUser.Name}");
            string imageString = Convert.ToBase64String(imageBytes);

            user = new User
            {
                FacebookId = facebookUser.Id,
                Name = facebookUser.Name,
                Household = household,
                Image = $"data:image/jpeg;base64,{imageString}"
            };

            user = await _userRepository.CreateAsync(user);
        }

        return user;
    }

    public async Task<User?> RegisterWithFacebookAndInviteCodeAsync(string accessToken, string inviteCode)
    {
        var user = await _userRepository.GeyByInviteCodeAsync(inviteCode);

        if (user is null)
            return null;

        FacebookMeResponse? facebookUser;
        try
        {
            facebookUser = await _client.GetFromJsonAsync<FacebookMeResponse>($"{FacebookUri}/me?access_token={Uri.EscapeDataString(accessToken)}");

            if (facebookUser is null)
                return null;
        } catch
        {
            throw new Exception("Invalid Facebook Token");
        }

        user.Name = facebookUser.Name;
        user.FacebookId = facebookUser.Id;
        user.InviteCode = null;

        user = await _userRepository.UpdateAsync(user);

        return user;
    }
}