namespace KucniSavetBackend.DTO.Requests.User;

public class UpdateUserRequest
{
    public string Id { get; set; } = default!;
    public string Name { get; set; } = default!;
    public string Image { get; set; } = default!;
    public string Email { get; set; } = default!;
}