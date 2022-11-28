using EFCoreDataAccess.Models;

namespace ContactListAPI.Validation.Interfaces;
/// <summary>
/// Interface for validation of Categories.
/// </summary>
public interface ICategoryValidator
{
    bool Validate(Category category);
    bool ValidateName(string name);
}
