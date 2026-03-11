namespace KucniSavetBackend.Domain;

public class User
{
    public string? Id { get; set; } = null;
    public string? Image { get; set; } = null;
    public string? Name { get; set; } = null;
    public string? Email { get; set; } = null;
    public Household? Household { get; set; } = null;
}
