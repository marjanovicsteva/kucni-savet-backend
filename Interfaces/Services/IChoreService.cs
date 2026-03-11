using KucniSavetBackend.DTO.Requests.User;
using KucniSavetBackend.DTO.Responses;

namespace KucniSavetBackend.Interfaces.Services;

public interface IChoreService
{
    Task<ChoreResponse?> GetByIdAsync(string id);
    Task<ChoreResponse> CreateAsync(CreateChoreRequest request);
    Task<ChoreResponse?> AddAssigneeToChore(string choreId, string assigneeId);
}