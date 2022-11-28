using ContactListAPI.Validation.Interfaces;
using EFCoreDataAccess.Data;
using EFCoreDataAccess.Models;
using Microsoft.EntityFrameworkCore.Metadata;

namespace ContactListAPI.Validation.Implementations;
/// <summary>
/// Class responsible for validating Subcategory db models.
/// </summary>
public class SubcategoryValidator : ISubcategoryValidator
{
    private readonly ContactListContext _dbContext;
    private readonly IEntityType _tableType;

    public SubcategoryValidator(ContactListContext dbContext)
    {
        _dbContext = dbContext;
        _tableType = _dbContext.Subcategories.EntityType;
    }
    /// <summary>
    /// Validates subcategory by validating every field.
    /// </summary>
    /// <param name="subcategory">Subcategory to validate.</param>
    /// <returns>True if every field is in correct format, false if otherwise or if subcategory is null.</returns>
    public bool Validate(Subcategory subcategory)
    {
        if (subcategory == null)
            return false;
        else
            return ValidateName(subcategory.Name);
    }
    /// <summary>
    /// Validates subcategory name by checking if its length is not greater than db table column max length.
    /// </summary>
    /// <param name="name">Name to validate.</param>
    /// <returns>Return true if name has correct length, otherwise false.</returns>
    public bool ValidateName(string name)
    {
        IProperty nameColumn = _tableType.GetProperty("Name");
        return name.Length <= nameColumn.GetMaxLength();
    }
}
