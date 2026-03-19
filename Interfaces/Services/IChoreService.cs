using KucniSavetBackend.Domain;
using KucniSavetBackend.Enums;

namespace KucniSavetBackend.Interfaces.Services;

public interface IChoreService
{
    Task<Chore?> GetByIdAsync(string id);
    Task<Chore?> CreateAsync(string name, Frequency frequency, string householdId);
    Task<Chore?> UpdateAsync(string id, string name, Frequency frequency);
    Task<Chore?> AddAssigneeToChore(string choreId, string assigneeId);
    Task<Chore?> RemoveAssigneFromChore(string choreId, string assigneeId);
}