using EFCoreDataAccess.Models;
using Microsoft.EntityFrameworkCore;

namespace EFCoreDataAccess.Data;
/// <summary>
/// Database context.
/// </summary>
public class ContactListContext : DbContext
{
    public ContactListContext(DbContextOptions options) : base(options) { }
    public DbSet<User> Users { get; set; } = null!;
    public DbSet<Person> People { get; set; } = null!;
    public DbSet<Contact> Contacts { get; set; } = null!;
    public DbSet<Category> Categories { get; set; } = null!;
    public DbSet<Subcategory> Subcategories { get; set; } = null!;
}
