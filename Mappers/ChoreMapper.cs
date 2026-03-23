using KucniSavetBackend.Domain;
using KucniSavetBackend.DTO.Responses;

namespace KucniSavetBackend.Mappers;

public static class ChoreMapper
{
    public static ChoreResponse ToResponse(Chore chore) => new ChoreResponse
    {
        Id = chore.Id ?? default!,
        Name = chore.Name,
        Frequency = chore.Frequency,
        LastDone = chore.LastDone,
        ToDo = chore.ToDo,
        Assignees = chore.Assignees.Select(assignee => UserMapper.ToResponse(assignee)).ToList(),
    };

    public static Chore ToDomain(ChoreResponse chore) => new Chore
    {
        Id = chore.Id,
        Name = chore.Name,
        Frequency = chore.Frequency,
        LastDone = chore.LastDone,
        Assignees = chore.Assignees.Select(assignee => UserMapper.ToDomain(assignee)).ToList()
    };
}
