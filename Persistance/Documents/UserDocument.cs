namespace KucniSavetBackend.Persistance.Documents;

public class UserDocument
{
    public string Id { get; set; } = default!;
    public string Image { get; set; } = default!;
    public string Name { get; set; } = default!;
    public string HouseholdId { get; set; } = default!;
    public string FacebookId { get ; set; } = default!;
    public string InviteCode { get; set; } = default!;
}