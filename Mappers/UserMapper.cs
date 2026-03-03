using KucniSavetBackend.DTO;
using KucniSavetBackend.Model;

namespace KucniSavetBackend.Mappers;

public static class UserMapper
{
    public static UserDto ToDto(User user) => new UserDto
    {
        Id = user.Id,
        Name = user.Name,
        Email = user.Email,
        Image = user.Image
    };

    public static User ToDomain(UserDto user) => new User
    {
        Id = user.Id,
        Name = user.Name,
        Email = user.Email,
        Image = user.Image
    };
}