using KucniSavetBackend.Domain;
using KucniSavetBackend.DTO.Requests.User;
using KucniSavetBackend.DTO.Responses;
using KucniSavetBackend.Exceptions;
using KucniSavetBackend.Interfaces.Repositories;
using KucniSavetBackend.Interfaces.Services;
using KucniSavetBackend.Mappers;
using KucniSavetBackend.Repositories.RavenDB;

namespace KucniSavetBackend.Services;

public class ChoreService : IChoreService
{
    private readonly IChoreRepository _choreRepository;
    private readonly IUserRepository _userRepository;

    public ChoreService(IChoreRepository choreRepository, IUserRepository userRepository)
    {
        _choreRepository = choreRepository;
        _userRepository = userRepository;
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

        if (chore is null)
            throw new OperationalException("Error creating a user");

        return ChoreMapper.ToResponse(chore);
    }

    public async Task<ChoreResponse> GetByIdAsync(string id)
    {
        var chore = await _choreRepository.GetByIdAsync(id);

        if (chore is null)
            throw new NotFoundException<Chore>(id);

        return ChoreMapper.ToResponse(chore);
    }

    public async Task<ChoreResponse> AddAssigneeToChore(string choreId, string assigneeId)
    {
        var chore = await _choreRepository.GetByIdAsync(choreId);

        if (chore is null)
            throw new NotFoundException<Chore>(choreId);

        var assignee = await _userRepository.GetByIdAsync(assigneeId);

        if (assignee is null)
            throw new NotFoundException<User>(assigneeId);

        if (!chore.Assignees.Any(a => a.Id == assignee.Id))
            chore.Assignees.Add(assignee);

        chore = await _choreRepository.UpdateAsync(chore);

        return ChoreMapper.ToResponse(chore);
    }
}