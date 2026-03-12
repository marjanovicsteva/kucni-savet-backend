using KucniSavetBackend.Domain;

namespace KucniSavetBackend.Interfaces.Repositories;

public interface IChoreRepository
{
    Task<Chore?> GetByIdAsync(string key, bool prefixed = false);
    Task<Chore?> CreateAsync(Chore chore);
    Task<Chore?> UpdateAsync(Chore chore);
    Task DeleteAsync(string key, bool prefixed = false);
    Task DeleteAsync(Chore chore);
}