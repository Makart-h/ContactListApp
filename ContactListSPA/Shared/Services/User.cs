namespace ContactListSPA.Shared.Services;
/// <summary>
/// Class storing the information of the current user.
/// </summary>
public class User
{
    public string Username { get; set; } = string.Empty;
    public string Token { get; set; } = string.Empty;
    public bool IsLoggedIn { get; set; } = false;
}
