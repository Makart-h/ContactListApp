using EFCoreDataAccess.Models;

namespace ContactListAPI.Validation.Interfaces;
/// <summary>
/// Interface for validation of Users.
/// </summary>
public interface IUserValidator
{
    bool Validate(User user);
    bool ValidateUsername(string username);
    bool ValidatePasswordHash(string passwordHash);
}
