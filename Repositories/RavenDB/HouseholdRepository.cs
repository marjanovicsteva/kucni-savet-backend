using KucniSavetBackend.Domain;
using KucniSavetBackend.Interfaces.Repositories;
using KucniSavetBackend.Persistance.Documents;
using Raven.Client.Documents.Session;

namespace KucniSavetBackend.Repositories.RavenDB;

public class HouseholdRepository : IHouseholdRepository
{
    private readonly IAsyncDocumentSession _session;
    private static readonly string _prefix = nameof(HouseholdDocument);
    public static string Id(string key) => $"{_prefix}s/{key}";

    public HouseholdRepository(IAsyncDocumentSession session)
    {
        _session = session;
    }

    public async Task<Household?> GetByIdAsync(string key, bool prefixed = false)
    {
        string id = prefixed ? key : Id(key);
        
        var doc = await _session.LoadAsync<HouseholdDocument>(id);

        if (doc is null)
            return null;

        return new Household
        {
            Id = doc.Id,
            Name = doc.Name
        };
    }

    public async Task<Household?> CreateAsync(Household household)
    {
        var doc = new HouseholdDocument
        {
            Id = household.Id,
            Name = household.Name
        };

        await _session.StoreAsync(doc);
        await _session.SaveChangesAsync();

        return await GetByIdAsync(doc.Id, true);
    }

    public async Task<Household?> UpdateAsync(Household household)
    {
        var doc = await _session.LoadAsync<HouseholdDocument>(household.Id);

        doc.Name = household.Name;

        await _session.SaveChangesAsync();

        return await GetByIdAsync(doc.Id, true);
    }

    public async Task DeleteAsync(string key, bool prefixed = false)
    {
        string id = prefixed ? key : Id(key);

        _session.Delete(id);

        await _session.SaveChangesAsync();
    }

    public async Task DeleteAsync(Household household)
    {
        await DeleteAsync(household.Id, true);
    }
}
