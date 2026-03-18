using KucniSavetBackend.Domain;
using KucniSavetBackend.DTO.Requests.User;
using KucniSavetBackend.DTO.Responses;
using KucniSavetBackend.Interfaces.Repositories;
using KucniSavetBackend.Interfaces.Services;
using KucniSavetBackend.Mappers;
using Microsoft.IdentityModel.Tokens;

namespace KucniSavetBackend.Services;

public class HouseholdService(IHouseholdRepository householdRepository) : IHouseholdService
{
    private readonly IHouseholdRepository _householdRepository = householdRepository;

    public async Task<Household?> CreateAsync(string name)
    {
        if (string.IsNullOrEmpty(name))
        {
            // Do validation in this layer
        }

        var household = new Household
        {
            Name = name
        };

        household = await _householdRepository.CreateAsync(household);
        
        return household;
    }

    public async Task<Household?> GetByIdAsync(string id)
    {
        var household = await _householdRepository.GetByIdAsync(id);
        return household;
    }

    public async Task<Household?> UpdateAsync(string id, string name)
    {
        var household = await _householdRepository.GetByIdAsync(id);

        if (household is null) return null;

        household.Name = name;

        household = await _householdRepository.UpdateAsync(household);

        return household;
    }
}