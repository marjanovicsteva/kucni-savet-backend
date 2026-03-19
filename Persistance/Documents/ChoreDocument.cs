using KucniSavetBackend.Enums;

namespace KucniSavetBackend.Persistance.Documents;

public class ChoreDocument
{
    public string? Id { get; set; }
    public string? HouseholdId { get; set; }
    public required string Name { get; set; }
    public required Frequency Frequency { get; set; }
    public DateTime? LastDone { get; set; }
    public List<string> AssigneesIds { get; set; } = [];
}