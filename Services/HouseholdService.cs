using KucniSavetBackend.Domain;
using KucniSavetBackend.DTO.Requests.User;
using KucniSavetBackend.DTO.Responses;
using KucniSavetBackend.Interfaces.Repositories;
using KucniSavetBackend.Interfaces.Services;
using KucniSavetBackend.Mappers;
using Microsoft.IdentityModel.Tokens;

namespace KucniSavetBackend.Services;

public class HouseholdService : IHouseholdService
{
    private readonly IHouseholdRepository _householdRepository;

    public HouseholdService(IHouseholdRepository householdRepository)
    {
        _householdRepository = householdRepository;
    }

    public async Task<HouseholdResponse> CreateAsync(CreateHouseholdRequest request)
    {
        if (string.IsNullOrEmpty(request.Name))
        {
            // Do validation in this layer
        }

        var household = new Household
        {
            Name = request.Name
        };

        var createdHousehold = await _householdRepository.CreateAsync(household);
        
        return HouseholdMapper.ToResponse(createdHousehold);
    }

    public async Task<HouseholdResponse?> GetByIdAsync(string id)
    {
        var household = await _householdRepository.GetByIdAsync(id);
        return household is not null ? HouseholdMapper.ToResponse(household) : null;
    }
}