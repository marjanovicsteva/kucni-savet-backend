using KucniSavetBackend.Domain;

namespace KucniSavetBackend.Interfaces.Repositories;

public interface IChoreRepository
{
    Task<Chore?> GetByIdAsync(string key, bool prefixed = false);
    Task<Chore?> CreateAsync(Chore chore);
    Task<Chore?> AddAssigneeAsync(string choreKey, string assigneeKey);
}