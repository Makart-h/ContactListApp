using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EFCoreDataAccess.Models;
/// <summary>
/// Database Person model.
/// </summary>
public class Person
{
    public int Id { get; set; }
    [MaxLength(50)]
    public string FirstName { get; set; } = null!;
    [MaxLength(100)]
    public string LastName { get; set; } = null!;
    [Required]
    [Column(TypeName = "date")]
    public DateTime Birthdate { get; set; }
}
