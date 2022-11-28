using ContactListAPI.Validation.Interfaces;
using EFCoreDataAccess.Data;
using EFCoreDataAccess.Models;
using Microsoft.EntityFrameworkCore.Metadata;
using System.Net.Mail;
using System.Text.RegularExpressions;

namespace ContactListAPI.Validation.Implementations;
/// <summary>
/// Class responsible for validating Contact db models.
/// </summary>
public class ContactValidator : IContactValidator
{
    private readonly ContactListContext _dbContext;
    private readonly IEntityType _tableType;
    private readonly IPersonValidator _personValidator;
    private readonly ICategoryValidator _categoryValidator;
    private readonly ISubcategoryValidator _subcategoryValidator;
    private readonly Regex _phoneNumberRegex;
    private readonly Regex _passwordRegex;
    public ContactValidator(ContactListContext dbContext, IPersonValidator personValidator, ICategoryValidator categoryValidator, ISubcategoryValidator subcategoryValidator)
    {
        _dbContext = dbContext;
        _tableType = _dbContext.Contacts.EntityType;
        _personValidator = personValidator;
        _categoryValidator = categoryValidator;
        _subcategoryValidator = subcategoryValidator;
        _phoneNumberRegex = new(@"^([+]?[\s0-9]+)?(\d{3}|[(]?[0-9]+[)])?([-]?[\s]?[0-9])+$");
        // Minimal length of 8, must contain at least 1 digit, 1 special character, 1 upercase letter and 1 lowercase letter.
        _passwordRegex = new("(?=^.{8,}$)(?=.*\\d)(?=.*[!@#$%^&*]+)(?![.\\n])(?=.*[A-Z])(?=.*[a-z]).*$");
    }
    /// <summary>
    /// Validates contact by validating every field.
    /// </summary>
    /// <param name="contact">Contact to validate.</param>
    /// <returns>True if every field is in correct format, false if otherwise or if contact is null.</returns>
    public bool Validate(Contact contact)
    {
        if (contact == null)
            return false;

        return ValidatePerson(contact.Person)
            && ValidateEmail(contact.Email)
            && ValidatePhoneNumber(contact.PhoneNumber)
            && ValidateCategory(contact.Category)
            && ValidateSubcategory(contact.Subcategory)
            && ValidatePasswordHash(contact.PasswordHash);
    }
    /// <summary>
    /// Invokes ICategoryValidator to check if category is valid.
    /// </summary>
    /// <param name="category">Category to validate.</param>
    /// <returns>True if ICategoryValidator deemed category valid, otherwise false.</returns>
    public bool ValidateCategory(Category category) => _categoryValidator.Validate(category);
    /// <summary>
    /// Uses System.Net.Mail.MailAdress constructor to check if email is in correct format
    /// and if it's lenght doesn't exceed db table coulumn max lentgth.
    /// </summary>
    /// <param name="email">Email to validate.</param>
    /// <returns>True if email is in correct format, otherwise false.</returns>
    public bool ValidateEmail(string email)
    {
        IProperty emailColumn = _tableType.GetProperty("Email");
        if (email.Length > emailColumn.GetMaxLength())
            return false;

        try
        {
            MailAddress validEmail = new(email);
            return true;
        }
        catch(Exception)
        {
            return false;
        }      
    }
    /// <summary>
    /// Checks if passwordHash length doesn't exceed db table coulumn max lentgth.
    /// </summary>
    /// <param name="passwordHash">Hashed password.</param>
    /// <returns>True if password is valid, otherwise false.</returns>
    public bool ValidatePasswordHash(string passwordHash)
    {
        IProperty passwordHashColumn = _tableType.GetProperty("PasswordHash");
        if (passwordHash.Length > passwordHashColumn.GetMaxLength())
            return false;
        else
            return true;
    }
    /// <summary>
    /// Invokes IPersonValidator to check if person is valid.
    /// </summary>
    /// <param name="person">Person to validate.</param>
    /// <returns>True if IPersonValidator deemed category valid, otherwise false.</returns>
    public bool ValidatePerson(Person person) => _personValidator.Validate(person);
    /// <summary>
    /// Checks if phoneNumber length doesn't exceed table column max length
    /// and checks it agains regex for validation.
    /// </summary>
    /// <param name="phoneNumber">Phone number to validate.</param>
    /// <returns>True if both regex validation and length validation is fulfiled, otherwise false.</returns>
    public bool ValidatePhoneNumber(string phoneNumber)
    {
        IProperty phoneColumn = _tableType.GetProperty("PhoneNumber");
        if (phoneNumber.Length > phoneColumn.GetMaxLength())
            return false;
        else
            return _phoneNumberRegex.IsMatch(phoneNumber);
    }
    /// <summary>
    /// Invokes ISubcategoryValidator to check if subcategory is valid.
    /// </summary>
    /// <param name="subcategory">Subcategory to vaidate.</param>
    /// <returns>True if ISubcategoryValidator deemed category valid, otherwise false.</returns>
    public bool ValidateSubcategory(Subcategory? subcategory)
    {
        if (subcategory == null)
            return true;
        else
            return _subcategoryValidator.Validate(subcategory);
    }
    /// <summary>
    /// Checks password against regex to check if it meets requirements:
    /// <list type="bullet">
    /// <item>at least 8 characters long,</item>
    /// <item>contains at least 1 lowercase letter,</item>
    /// <item>contains at least 1 uppercase letter,</item>
    /// <item>contains at least 1 special character.</item>
    /// </list>
    /// </summary>
    /// <param name="password">Raw password to validate.</param>
    /// <returns>True if password meets requirements, otherwise false.</returns>
    public bool ValidatePassword(string password) => _passwordRegex.IsMatch(password);
}
