namespace ContactListSPA.Shared.Models;
/// <summary>
/// Person model for API requests.
/// </summary>
public class PersonModel
{
    public int Id { get; set; }
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public DateTime Birthdate { get; set; }
}
