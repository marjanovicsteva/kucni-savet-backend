namespace KucniSavetBackend.Persistance.Documents;

public class HouseholdDocument
{
    public required string Id { get; set; }
    public string Name { get; set; } = default!;
}