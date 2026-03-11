using KucniSavetBackend.Enums;

namespace KucniSavetBackend.Domain;

public class Chore
{
    public string? Id { get; set; }
    public required string Name { get; set; }
    public required Frequency Frequency { get; set; }
    public DateTime? LastDone { get; set; }
    public List<User> Assignees { get; set; } = [];
}