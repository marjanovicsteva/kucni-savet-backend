using KucniSavetBackend.Model;
using KucniSavetBackend.DTO;
using Raven.Client.Documents;
using KucniSavetBackend.Responses;
using KucniSavetBackend.Mappers;

namespace KucniSavetBackend.Services;

public class UserService
{
    private readonly IDocumentStore _store;
    
    public UserService(IDocumentStore store)
    {
        _store = store;
    }

    public async Task<UsersResponse> GetAllAsync(Pagination? pagination)
    {
        using var session = _store.OpenAsyncSession();

        IQueryable<User> userQuery = session.Query<User>()
            .OrderBy(user => user.Name);

        var count = await userQuery.CountAsync();

        if (pagination is not null)
        {
            pagination.CalculateTotalPages(count);
            int skip = (pagination.PageNumber - 1) * pagination.PageSize;
            userQuery = userQuery
                .Skip(skip)
                .Take(pagination.PageSize);
        }

        var users = await userQuery.ToListAsync();
        var usersData = users.Select(UserMapper.ToDto).ToList();

        return new UsersResponse
        {
            Data = usersData,
            Count = count,
            Pagination = pagination
        };
    }

    public async Task<User> Get(string id)
    {
        using var session = _store.OpenAsyncSession();

        var user = await session.LoadAsync<User>(id);
        
        return user;
    }

    public async Task<User> Create(User user)
    {
        using var session = _store.OpenAsyncSession();

        await session.StoreAsync(user);
        await session.SaveChangesAsync();

        return user;
    }

    public async Task<User> Update(User user)
    {
        using var session = _store.OpenAsyncSession();



        throw new NotImplementedException();
    }
}