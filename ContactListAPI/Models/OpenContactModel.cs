namespace ContactListAPI.Models;
/// <summary>
/// OpenContact model for the unauthorized http requests.
/// </summary>
public class OpenContactModel
{
    public int Id { get; set; }
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string Category { get; set; } = string.Empty;
    public string? Subcategory { get; set; } = null;

}
