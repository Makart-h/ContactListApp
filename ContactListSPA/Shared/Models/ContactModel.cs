using System.ComponentModel.DataAnnotations;

namespace ContactListSPA.Shared.Models;
/// <summary>
/// Contact model for API requests.
/// </summary>
public class ContactModel
{
    public PersonModel Person { get; set; } = new();
    [Required(ErrorMessage = "Enter email")]
    [EmailAddress]
    public string Email { get; set; } = string.Empty;
    [Required(ErrorMessage = "Enter phone number")]
    [RegularExpression(@"^([+]?[\s0-9]+)?(\d{3}|[(]?[0-9]+[)])?([-]?[\s]?[0-9])+$",
        ErrorMessage = "Not a valid phone number!")]
    public string PhoneNumber { get; set; } = string.Empty;
    public CategoryModel Category { get; set; } = new();
    public SubcategoryModel? Subcategory { get; set; } = new();
    [Required(ErrorMessage = "Enter password")]
    [RegularExpression("(?=^.{8,}$)(?=.*\\d)(?=.*[!@#$%^&*]+)(?![.\\n])(?=.*[A-Z])(?=.*[a-z]).*$",
        ErrorMessage = "Password must contain at least 8 characters including: 1 digit, 1 special character, 1 uppercase letter and 1 lowercase letter.")]
    public string PasswordHash { get; set; } = string.Empty;
}
