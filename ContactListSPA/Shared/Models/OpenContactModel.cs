namespace ContactListSPA.Shared.Models;
/// <summary>
/// OpenContact model for API requests.
/// </summary>
public class OpenContactModel
{
    public int Id { get; set; }
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string Category { get; set; } = string.Empty;
    public string? Subcategory { get; set; } = null;
}
