using System.ComponentModel.DataAnnotations;

namespace EFCoreDataAccess.Models;
/// <summary>
/// Database Subcategory model.
/// </summary>
public class Subcategory
{
    public int Id { get; set; }
    [MaxLength(100)]
    public string Name { get; set; } = null!;
}
