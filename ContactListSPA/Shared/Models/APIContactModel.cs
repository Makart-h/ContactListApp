namespace ContactListSPA.Shared.Models;
/// <summary>
/// Contact model for API POST/UPDATE requests.
/// </summary>
public class APIContactModel
{
    public int PersonId { get; set; }
    public string Email { get; set; } = string.Empty;
    public string PhoneNumber { get; set; } = string.Empty;
    public int CategoryId { get; set; }
    public int? SubcategoryID { get; set; } = null;
    public string Password { get; set; } = string.Empty;
}
