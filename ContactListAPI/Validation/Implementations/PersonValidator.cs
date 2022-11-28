using ContactListAPI.Validation.Interfaces;
using EFCoreDataAccess.Data;
using EFCoreDataAccess.Models;
using Microsoft.EntityFrameworkCore.Metadata;
using System.Text.RegularExpressions;

namespace ContactListAPI.Validation.Implementations;
/// <summary>
/// Class responsible for validating Person db models.
/// </summary>
public class PersonValidator : IPersonValidator
{
    private readonly ContactListContext _dbContext;
    private readonly IEntityType _tableType;
    private readonly Regex _nameRegex;

    public PersonValidator(ContactListContext dbContext)
    {
        _dbContext = dbContext;
        _tableType = _dbContext.People.EntityType;
        _nameRegex = new(@"^[A-Z][a-z]*$");
    }
    /// <summary>
    /// Validates person by validating every field.
    /// </summary>
    /// <param name="person">Person to validate.</param>
    /// <returns>True if every field is in correct format, false if otherwise or if person is null.</returns>
    public bool Validate(Person person)
    {
        if (person == null)
            return false;
        else
            return ValidateBirthdate(person.Birthdate)
                && ValidateFirstName(person.FirstName)
                && ValidateLastName(person.LastName);
    }
    /// <summary>
    /// Validates birthdate by checking if it's not from the future.
    /// </summary>
    /// <param name="date">Date to validate.</param>
    /// <returns>True if birthdate is not greater then DateTime.Now, otherwise false.</returns>
    public bool ValidateBirthdate(DateTime date) => date <= DateTime.Now;
    /// <summary>
    /// Validates first name by checking if it's length is not greater than table column max length
    /// and checks it against regex with those requirements:
    /// <list type="bullet">
    /// <item>contains only letters,</item>
    /// <item>starts with an uppercase letter.</item>
    /// </list>
    /// </summary>
    /// <param name="firstName">First name to validate.</param>
    /// <returns>True if length is correct and regex is matched, otherwise false.</returns>
    public bool ValidateFirstName(string firstName)
    {
        IProperty firstNameColumn = _tableType.GetProperty("FirstName");
        if (firstName.Length > firstNameColumn.GetMaxLength())
            return false;
        else
            return _nameRegex.IsMatch(firstName);
    }
    /// <summary>
    /// Validates last name by checking if it's length is not greater than table column max length
    /// and checks it against regex with those requirements:
    /// <list type="bullet">
    /// <item>contains only letters,</item>
    /// <item>starts with an uppercase letter.</item>
    /// </list>
    /// </summary>
    /// <param name="lastName">Last name to validate.</param>
    /// <returns>True if length is correct and regex is matched, otherwise false.</returns>
    public bool ValidateLastName(string lastName)
    {
        IProperty firstNameColumn = _tableType.GetProperty("LastName");
        if (lastName.Length > firstNameColumn.GetMaxLength())
            return false;
        else
            return _nameRegex.IsMatch(lastName);
    }
}
