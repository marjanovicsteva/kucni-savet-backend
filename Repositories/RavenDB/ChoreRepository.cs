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

    public async Task<Chore?> CreateAsync(Chore? chore)
    {
        if (chore is null)
            return null;

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
                Image = assignee.Image,
                Name = assignee.Name
            }).ToList()
        };
    }

    public async Task<Chore?> UpdateAsync(Chore chore)
    {
        var doc = await _session.LoadAsync<ChoreDocument>(chore.Id);
        
        doc.Name = chore.Name;
        doc.Frequency = chore.Frequency;
        doc.LastDone = chore.LastDone;
        doc.AssigneesIds = chore.Assignees.Select(assignee => assignee.Id).ToList();

        await _session.SaveChangesAsync();

        return await GetByIdAsync(doc.Id, true);
    }

    public async Task DeleteAsync(string key, bool prefixed = false)
    {
        string id = prefixed ? key : Id(key);
        _session.Delete(id);
        await _session.SaveChangesAsync();
    }

    public async Task DeleteAsync(Chore chore)
    {
        await DeleteAsync(chore.Id, true);
    }
}