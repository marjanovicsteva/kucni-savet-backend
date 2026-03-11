using KucniSavetBackend.Domain;
using KucniSavetBackend.Interfaces.Repositories;
using KucniSavetBackend.Persistance.Documents;
using Raven.Client.Documents.Session;

namespace KucniSavetBackend.Repositories.RavenDB;

public class HouseholdRepository : IHouseholdRepository
{
    private readonly IAsyncDocumentSession _session;

    public HouseholdRepository(IAsyncDocumentSession session)
    {
        _session = session;
    }

    public async Task<Household?> GetByIdAsync(string id)
    {
        var doc = await _session.LoadAsync<HouseholdDocument>(id);

        return new Household
        {
            Id = doc.Id,
            Name = doc.Name
        };
    }

    public async Task<Household> CreateAsync(Household household)
    {
        var doc = new HouseholdDocument
        {
            Id = household.Id,
            Name = household.Name
        };

        await _session.StoreAsync(doc);
        await _session.SaveChangesAsync();

        household.Id = doc.Id;
        return household;
    }
}