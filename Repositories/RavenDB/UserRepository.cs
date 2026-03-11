using KucniSavetBackend.Domain;
using KucniSavetBackend.Interfaces.Repositories;
using KucniSavetBackend.Persistance.Documents;
using Raven.Client.Documents.Session;

namespace KucniSavetBackend.Repositories.RavenDB;

public class UserRepository : IUserRepository
{
    private readonly IAsyncDocumentSession _session;

    public UserRepository(IAsyncDocumentSession session)
    {
        _session = session;
    }

    public async Task<User?> GetByIdAsync(string id)
    {
        var doc = await _session.LoadAsync<UserDocument>(id);

        if (doc is null)
            return null;

        var user = new User
        {
            Id = doc.Id,
            Name = doc.Name,
            Email = doc.Email,
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

    public async Task<User> CreateAsync(User user)
    {
        var doc = new UserDocument
        {
            Id = user.Id,
            Name = user.Name,
            Email = user.Email,
            Image = user.Image,
            HouseholdId = user.Household?.Id
        };

        await _session.StoreAsync(doc);
        await _session.SaveChangesAsync();

        user.Id = doc.Id;
        return user;
    }
}