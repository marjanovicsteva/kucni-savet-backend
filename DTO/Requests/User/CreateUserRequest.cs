namespace KucniSavetBackend.DTO.Requests.User;

public class CreateUserRequest
{
    public required string Name { get; set; }
    public string? Image { get; set; } = null;
    public required string Email { get; set; }
    public required string HouseholdName { get; set; }
}