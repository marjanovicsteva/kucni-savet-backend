namespace KucniSavetBackend.DTO.Responses;

public class UserResponse
{
    public string Id { get; set; } = default!;
    public string Name { get; set; } = default!;
    public string Email { get; set; } = default!;
    public string? Image { get; set; } = null;
    public HouseholdResponse? Household { get; set; } = new();
}