using KucniSavetBackend.Enums;

namespace KucniSavetBackend.DTO.Responses;

public class ChoreResponse
{
    public required string Id { get; set; }
    public required string Name { get; set; }
    public required Frequency Frequency { get; set; }
    public DateTime? LastDone { get; set; }
    public required List<UserResponse> Assignees { get; set; }
}
