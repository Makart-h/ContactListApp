using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EFCoreDataAccess.Models;
/// <summary>
/// Database Contact model.
/// </summary>
[Index(nameof(Email), IsUnique = true)]
public class Contact
{
    public int Id { get; set; }
    public Person Person { get; set; } = null!;
    [MaxLength(256)]
    public string Email { get; set; } = null!;
    [MaxLength(15)]
    [Column(TypeName = "varchar(15)")]
    public string PhoneNumber { get; set; } = null!;
    public Category Category { get; set; } = null!;
    public Subcategory? Subcategory { get; set; }
    [MaxLength(256)]
    public string PasswordHash { get; set; } = null!;
}
