using ContactListAPI.Validation.Interfaces;
using EFCoreDataAccess.Data;
using EFCoreDataAccess.Models;
using Microsoft.EntityFrameworkCore.Metadata;

namespace ContactListAPI.Validation.Implementations;
/// <summary>
/// Class reposible for validation of Category db models.
/// </summary>
public class CategoryValidator : ICategoryValidator
{
    private readonly ContactListContext _dbContext;
    private readonly IEntityType _tableType;

    public CategoryValidator(ContactListContext dbContext)
    {
        _dbContext = dbContext;
        _tableType = _dbContext.Categories.EntityType;
    }
    /// <summary>
    /// Validates category by validating every field.
    /// </summary>
    /// <param name="category">Category to validate.</param>
    /// <returns>True if every field is in correct format, false if otherwise or category is null.</returns>
    public bool Validate(Category category)
    {
        if (category == null)
            return false;
        else
            return ValidateName(category.Name);
    }
    /// <summary>
    /// Checks if provided name doesn't exceed db table column max length.
    /// </summary>
    /// <param name="name">Name to validate.</param>
    /// <returns>True if name is in correct format, otherwise false.</returns>
    public bool ValidateName(string name)
    {
        IProperty nameColumn = _tableType.GetProperty("Name");
        return name.Length <= nameColumn.GetMaxLength();
    }
}
