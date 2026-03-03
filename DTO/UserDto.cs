using KucniSavetBackend.Model;

namespace KucniSavetBackend.DTO;

public class UserDto
{
    public string Id { get; set; } = default!;
    public string Image { get; set; } = default!;
    public string Name { get; set; } = default!;
    public string Email { get; set; } = default!;

    public static User ToModel(UserDto userDto)
    {
        return new User
        {
            Id = userDto.Id,
            Email = userDto.Email,
            Name = userDto.Name,
            Image = userDto.Image
        };
    }
}
