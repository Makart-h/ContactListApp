using System.ComponentModel.DataAnnotations;

namespace ContactListSPA.Shared.Models;
/// <summary>
/// Login model for API requests.
/// </summary>
public class LoginModel
{

    [Required(ErrorMessage ="Enter username")]
    public string Username { get; set; } = string.Empty;
    [Required(ErrorMessage = "Enter password")]
    [RegularExpression("(?=^.{8,}$)(?=.*\\d)(?=.*[!@#$%^&*]+)(?![.\\n])(?=.*[A-Z])(?=.*[a-z]).*$",
        ErrorMessage ="Password must contain at least 8 characters including: 1 digit, 1 special character, 1 uppercase letter and 1 lowercase letter.")]
    public string Password { get; set; } = string.Empty;
}
