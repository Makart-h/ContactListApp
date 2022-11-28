namespace ContactListAPI.Models;
/// <summary>
/// Person model for the http requests.
/// </summary>
public class PersonModel
{
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public DateTime Birthdate { get; set; }
}
