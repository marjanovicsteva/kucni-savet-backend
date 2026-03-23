namespace KucniSavetBackend.DTO.Requests.User;

public class UpdateUserRequest
{
    public string Name { get; set; } = default!;
    public string Image { get; set; } = default!;
}