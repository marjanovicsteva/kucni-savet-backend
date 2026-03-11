using KucniSavetBackend.Domain;

namespace KucniSavetBackend.Interfaces.Repositories;

public interface IHouseholdRepository
{
    Task<Household?> GetByIdAsync(string key, bool prefixed = false);
    Task<Household?> CreateAsync(Household household);
}