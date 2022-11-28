using ContactListAPI.Validation.Interfaces;
using EFCoreDataAccess.Data;
using EFCoreDataAccess.Models;
using Microsoft.EntityFrameworkCore.Metadata;

namespace ContactListAPI.Validation.Implementations;
/// <summary>
/// Class responsible for validating User db models.
/// </summary>
public class UserValidator : IUserValidator
{
    private readonly ContactListContext _dbContext;
    private readonly IEntityType _tableType;

    public UserValidator(ContactListContext dbContext)
    {
        _dbContext = dbContext;
        _tableType = _dbContext.Users.EntityType;
    }
    /// <summary>
    /// Validates user by validating every field.
    /// </summary>
    /// <param name="user">User to validate.</param>
    /// <returns>True if every field is in correct format, false if otherwise or if user is null.</returns>
    public bool Validate(User user)
    {
        if (user == null)
            return false;
        else
            return ValidatePasswordHash(user.PasswordHash) && ValidateUsername(user.Username);
    }
    /// <summary>
    /// Checks if passwordHash length doesn't exceed db table coulumn max lentgth.
    /// </summary>
    /// <param name="passwordHash">Hashed password.</param>
    /// <returns>True if password has correct length, otherwise false.</returns>
    public bool ValidatePasswordHash(string passwordHash)
    {
        IProperty passwordHashColumn = _tableType.GetProperty("PasswordHash");
        return passwordHash.Length <= passwordHashColumn.GetMaxLength();
    }
    /// <summary>
    /// Checks if username length doesn't exceed db table coulumn max lentgth.
    /// </summary>
    /// <param name="username">Username to validate.</param>
    /// <returns>True if username has correct length, otherwise false.</returns>
    public bool ValidateUsername(string username)
    {
        IProperty usernameColumn = _tableType.GetProperty("Username");
        return username.Length <= usernameColumn.GetMaxLength();
    }
}
