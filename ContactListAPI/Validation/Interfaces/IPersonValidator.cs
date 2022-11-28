using EFCoreDataAccess.Models;

namespace ContactListAPI.Validation.Interfaces;
/// <summary>
/// Interface for validation of People.
/// </summary>
public interface IPersonValidator
{
    bool Validate(Person person);
    bool ValidateFirstName(string firstName);
    bool ValidateLastName(string lastName);
    bool ValidateBirthdate(DateTime date);
}
