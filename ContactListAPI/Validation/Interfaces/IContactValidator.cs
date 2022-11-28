using EFCoreDataAccess.Models;

namespace ContactListAPI.Validation.Interfaces;
/// <summary>
/// Interface for validation of Contacts.
/// </summary>
public interface IContactValidator
{
    bool Validate(Contact contact);
    bool ValidatePerson(Person person);
    bool ValidateEmail(string email);
    bool ValidatePhoneNumber(string phoneNumber);
    bool ValidateCategory(Category category);
    bool ValidateSubcategory(Subcategory? subcategory);
    bool ValidatePasswordHash(string passwordHash);
    bool ValidatePassword(string password);
}
