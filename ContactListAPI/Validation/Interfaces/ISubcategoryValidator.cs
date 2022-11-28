using EFCoreDataAccess.Models;

namespace ContactListAPI.Validation.Interfaces;
/// <summary>
/// Interface for validation of Subcategories.
/// </summary>
public interface ISubcategoryValidator
{
    bool Validate(Subcategory subcategory);
    bool ValidateName(string name);
}
