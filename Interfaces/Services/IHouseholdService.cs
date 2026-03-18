using KucniSavetBackend.Domain;
using KucniSavetBackend.DTO.Requests.User;
using KucniSavetBackend.DTO.Responses;

namespace KucniSavetBackend.Interfaces.Services;

public interface IHouseholdService
{
    Task<Household?> GetByIdAsync(string id);
    Task<Household?> CreateAsync(string name);
    Task<Household?> UpdateAsync(string id, string name);
}