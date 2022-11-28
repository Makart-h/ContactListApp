using System.ComponentModel.DataAnnotations;

namespace EFCoreDataAccess.Models;
/// <summary>
/// Database User model.
/// </summary>
public class User
{
    public int Id { get; set; }
    [MaxLength(50)]
    public string Username { get; set; } = null!;
    [MaxLength(256)]
    public string PasswordHash { get; set; } = null!;
}
