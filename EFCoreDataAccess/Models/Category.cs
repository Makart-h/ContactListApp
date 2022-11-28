using System.ComponentModel.DataAnnotations;

namespace EFCoreDataAccess.Models;
/// <summary>
/// Database Category model.
/// </summary>
public class Category
{
    public int Id { get; set; }
    [MaxLength(100)]
    public string Name { get; set; } = null!;
}
