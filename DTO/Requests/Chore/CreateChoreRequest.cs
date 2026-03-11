using KucniSavetBackend.Enums;

namespace KucniSavetBackend.DTO.Requests.User;

public class CreateChoreRequest
{
    public required string Name { get; set; }
    public required Frequency Frequency { get; set; }
}