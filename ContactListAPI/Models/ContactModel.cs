namespace ContactListAPI.Models;
/// <summary>
/// Contact model for the http requests.
/// </summary>
public class ContactModel
{
    public int PersonId { get; set; }
    public string Email { get; set; } = string.Empty;
    public string PhoneNumber { get; set; } = string.Empty;
    public int CategoryId { get; set; }
    public int? SubcategoryID { get; set; } = null;
    public string Password { get; set; } = string.Empty;
}
