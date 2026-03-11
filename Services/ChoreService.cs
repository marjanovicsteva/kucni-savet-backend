using KucniSavetBackend.Domain;
using KucniSavetBackend.DTO.Requests.User;
using KucniSavetBackend.DTO.Responses;
using KucniSavetBackend.Interfaces.Repositories;
using KucniSavetBackend.Interfaces.Services;
using KucniSavetBackend.Mappers;

namespace KucniSavetBackend.Services;

public class ChoreService : IChoreService
{
    private readonly IChoreRepository _choreRepository;

    public ChoreService(IChoreRepository choreRepository)
    {
        _choreRepository = choreRepository;    
    }

    public async Task<ChoreResponse> CreateAsync(CreateChoreRequest request)
    {
        if (string.IsNullOrEmpty(request.Name))
        {
            // Do valuidateion
        }

        var chore = new Chore
        {
            Name = request.Name,
            Frequency = request.Frequency
        };

        chore = await _choreRepository.CreateAsync(chore);

        return ChoreMapper.ToResponse(chore);
    }

    public async Task<ChoreResponse?> GetByIdAsync(string id)
    {
        var chore = await _choreRepository.GetByIdAsync(id);
        return chore is not null ? ChoreMapper.ToResponse(chore) : null;
    }

    public async Task<ChoreResponse?> AddAssigneeToChore(string choreId, string assigneeId)
    {
        var chore = await _choreRepository.AddAssigneeAsync(choreId, assigneeId);
        return chore is not null ? ChoreMapper.ToResponse(chore) : null; 
    }
}