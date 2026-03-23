using KucniSavetBackend.Domain;
using KucniSavetBackend.DTO.Requests.User;
using KucniSavetBackend.Enums;
using KucniSavetBackend.Exceptions;
using KucniSavetBackend.Interfaces.Repositories;
using KucniSavetBackend.Interfaces.Services;
using KucniSavetBackend.Repositories.RavenDB;

namespace KucniSavetBackend.Services;

public class ChoreService(IChoreRepository choreRepository, IUserRepository userRepository) : IChoreService
{
    private readonly IChoreRepository _choreRepository = choreRepository;
    private readonly IUserRepository _userRepository = userRepository;

    public async Task<Chore?> CreateAsync(string name, Frequency frequency, string householdId)
    {
        if (string.IsNullOrEmpty(name))
        {
            // Do valuidateion
        }

        var chore = new Chore
        {
            HouseholdId = householdId,
            Name = name,
            Frequency = frequency,
        };

        chore = await _choreRepository.CreateAsync(chore);

        return chore;
    }
    
    public async Task<Chore?> UpdateAsync(string id, string name, Frequency frequency)
    {
        var chore = new Chore
        {
            Id = id,
            Name = name,
            Frequency = frequency
        };

        chore = await _choreRepository.UpdateAsync(chore);

        return chore;
    }

    public async Task<Chore?> GetByIdAsync(string id)
    {
        var chore = await _choreRepository.GetByIdAsync(id) ?? throw new NotFoundException<Chore>(id);
        return chore;
    }

    public async Task<Chore?> AddAssignee(string choreId, string assigneeId)
    {
        var chore = await _choreRepository.GetByIdAsync(choreId) ?? throw new NotFoundException<Chore>(choreId);
        var assignee = await _userRepository.GetByIdAsync(assigneeId) ?? throw new NotFoundException<User>(assigneeId);

        if (!chore.Assignees.Any(a => a.Id == assignee.Id))
            chore.Assignees.Add(assignee);

        chore = await _choreRepository.UpdateAsync(chore);

        return chore;
    }

    public async Task<Chore?> RemoveAssigne(string choreId, string assigneeId)
    {
        var chore = await _choreRepository.GetByIdAsync(choreId) ?? throw new NotFoundException<Chore>(choreId);
        chore.Assignees.RemoveAll(assignee => assignee.Id == UserRepository.Id(assigneeId));

        chore = await _choreRepository.UpdateAsync(chore);

        return chore;
    }

    public async Task<Chore?> MarkAsDone(string choreId)
    {
        var chore = await _choreRepository.GetByIdAsync(choreId) ?? throw new NotFoundException<Chore>(choreId);
        chore.LastDone = DateTime.UtcNow;

        chore = await _choreRepository.UpdateAsync(chore);

        return chore;
    }
}