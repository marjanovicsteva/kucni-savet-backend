using KucniSavetBackend.DTO;

namespace KucniSavetBackend.Responses;

public class UsersResponse : ResponseBase<List<UserDto>>
{
    public int Count { get; set; } = default!;
    public Pagination? Pagination { get; set; } = default!;
}