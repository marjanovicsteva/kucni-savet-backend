using KucniSavetBackend.DTO.Requests.User;
using KucniSavetBackend.DTO.Responses;

namespace KucniSavetBackend.Interfaces.Services;

public interface IHouseholdService
{
    Task<HouseholdResponse> GetByIdAsync(string id);
    Task<HouseholdResponse> CreateAsync(CreateHouseholdRequest request);
}