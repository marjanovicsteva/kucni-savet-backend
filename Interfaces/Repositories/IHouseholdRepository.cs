using KucniSavetBackend.Domain;

namespace KucniSavetBackend.Interfaces.Repositories;

public interface IHouseholdRepository
{
    Task<Household?> GetByIdAsync(string id);
    Task<Household> CreateAsync(Household household);
}