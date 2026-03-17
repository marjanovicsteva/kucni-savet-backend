using KucniSavetBackend.Domain;
using KucniSavetBackend.Interfaces.Repositories;
using KucniSavetBackend.Persistance.Documents;
using Raven.Client.Documents;
using Raven.Client.Documents.Session;

namespace KucniSavetBackend.Repositories.RavenDB;

public class UserRepository : IUserRepository
{
    private readonly IAsyncDocumentSession _session;
    private static readonly string _prefix = nameof(UserDocument);
    public static string Id(string key) => $"{_prefix}s/{key}";
    public UserRepository(IAsyncDocumentSession session)
    {
        _session = session;
    }

    public async Task<User?> GetByIdAsync(string key, bool prefixed = false)
    {
        string id = prefixed ? key : Id(key);

        var doc = await _session.LoadAsync<UserDocument>(id);

        if (doc is null)
            return null;

        var user = new User
        {
            Id = doc.Id,
            Name = doc.Name,
            Image = doc.Image
        };

        if (doc.HouseholdId is not null)
        {
            var household = await _session.LoadAsync<HouseholdDocument>(doc.HouseholdId);
            user.Household = new Household
            {
                Id = household.Id,
                Name = household.Name
            };
        }

        return user;
    }

    public async Task<User?> GetByFacebookIdAsync(string facebookId)
    {
        var doc = await _session.Query<UserDocument>()
            .Where(user => user.FacebookId == facebookId)
            .FirstOrDefaultAsync();

        if (doc is null) return null;

        var user = new User
        {
            Id = doc.Id,
            Name = doc.Name,
            Image = doc.Image
        };

        if (doc.HouseholdId is not null)
        {
            var household = await _session.LoadAsync<HouseholdDocument>(doc.HouseholdId);
            user.Household = new Household
            {
                Id = household.Id,
                Name = household.Name
            };
        }

        return user;
    }

    public async Task<User?> CreateAsync(User user)
    {
        var doc = new UserDocument
        {
            Id = user.Id,
            Name = user.Name,
            Image = user.Image,
            FacebookId = user.FacebookId,
            HouseholdId = user.Household?.Id
        };

        await _session.StoreAsync(doc);
        await _session.SaveChangesAsync();

        return await GetByIdAsync(doc.Id, true);
    }

    public async Task<User?> UpdateAsync(User user)
    {
        var doc = await _session.LoadAsync<UserDocument>(user.Id);

        doc.Name = user.Name;
        doc.Image = user.Image;

        await _session.SaveChangesAsync();

        return await GetByIdAsync(doc.Id, true);
    }

    public async Task DeleteAsync(string key, bool prefixed = false)
    {
        string id = prefixed ? key : Id(key);

        _session.Delete(id);

        await _session.SaveChangesAsync();
    }

    public async Task DeleteAsync(User user)
    {
        await DeleteAsync(user.Id, true);
    }
}