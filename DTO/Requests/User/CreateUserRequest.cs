namespace KucniSavetBackend.DTO.Requests.User;

public class CreateUserRequest
{
    public required string FacebookAccessToken { get; set; }
    public required string HouseholdName { get; set; }
}