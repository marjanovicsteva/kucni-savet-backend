namespace KucniSavetBackend.DTO;

public class Pagination
{
    public int PageNumber { get; set; } = 1;
    public int PageSize { get; set; } = 5;
    public int? TotalPages { get; set; }

    public void CalculateTotalPages(int totalCount)
    {
        TotalPages = (int)Math.Ceiling((double)totalCount / PageSize);
    }
}