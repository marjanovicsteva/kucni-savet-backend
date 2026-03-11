using KucniSavetBackend.Domain;
using KucniSavetBackend.Interfaces.Repositories;
using KucniSavetBackend.Persistance.Documents;
using KucniSavetBackend.Repositories.RavenDB;
using Raven.Client.Documents.Session;

namespace KucniSavetBackend.Repositories;

public class ChoreRepository : IChoreRepository
{
    private readonly IAsyncDocumentSession _session;
    private static readonly string _prefix = nameof(ChoreDocument);
    public static string Id(string key) => $"{_prefix}s/{key}";

    public ChoreRepository(IAsyncDocumentSession session)
    {
        _session = session;
    }

    public async Task<Chore?> CreateAsync(Chore chore)
    {
        var assignees = chore.Assignees;
        var doc = new ChoreDocument
        {
            Frequency = chore.Frequency,
            Name = chore.Name,
            LastDone = chore.LastDone,
            AssigneesIds = chore.Assignees.Select(assignee => assignee.Id).ToList()
        };

        await _session.StoreAsync(doc);
        await _session.SaveChangesAsync();

        return await GetByIdAsync(doc.Id, true);
    }

    public async Task<Chore?> GetByIdAsync(string key, bool prefixed = false)
    {
        string id = prefixed ? key : Id(key);

        var doc = await _session.LoadAsync<ChoreDocument>(id);
        var assignees = await _session.LoadAsync<UserDocument>(doc.AssigneesIds);

        return new Chore
        {
            Id = doc.Id,
            Name = doc.Name,
            Frequency = doc.Frequency,
            LastDone = doc.LastDone,
            Assignees = assignees.Values.Select(assignee => new User
            {
                Id = assignee.Id,
                Email = assignee.Email,
                Image = assignee.Image,
                Name = assignee.Name
            }).ToList()
        };
    }
    
    public async Task<Chore?> AddAssigneeAsync(string choreId, string assigneeId)
    {
        var chore = await _session.LoadAsync<ChoreDocument>(Id(choreId));
        assigneeId = UserRepository.Id(assigneeId);

        if (chore.AssigneesIds.Contains(assigneeId))
            return await GetByIdAsync(choreId);

        chore.AssigneesIds.Add(assigneeId);

        await _session.SaveChangesAsync();
        
        return await GetByIdAsync(chore.Id, true);
    }
}