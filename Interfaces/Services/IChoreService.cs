using KucniSavetBackend.Domain;
using KucniSavetBackend.DTO.Requests.User;
using KucniSavetBackend.DTO.Responses;

namespace KucniSavetBackend.Interfaces.Services;

public interface IChoreService
{
    Task<Chore?> GetByIdAsync(string id);
    Task<Chore?> CreateAsync(CreateChoreRequest request);
    Task<Chore?> AddAssigneeToChore(string choreId, string assigneeId);
}