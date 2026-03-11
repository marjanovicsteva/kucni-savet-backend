using KucniSavetBackend.Enums;

namespace KucniSavetBackend.DTO.Requests.User;

public class UpdateChoreRequest
{
    public required string Name { get; set; }
    public required Frequency Frequency { get; set; }
}