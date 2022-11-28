using ContactListAPI.Models;
using ContactListAPI.Repositories.Data.Interfaces;
using ContactListAPI.Validation.Interfaces;
using EFCoreDataAccess.Data;
using EFCoreDataAccess.Models;
using Microsoft.EntityFrameworkCore;

namespace ContactListAPI.Repositories.Data.Implementations;
/// <summary>
/// Class responsible for comunication with the Contacts table.
/// </summary>
public class ContactRepository : IContactRepository
{
    private readonly ContactListContext _dbContext;
    private readonly IContactValidator _validator;
    public ContactRepository(ContactListContext dbContext, IContactValidator validator)
    {
        _dbContext = dbContext;
        _validator = validator;
    }
    /// <summary>
    /// Tries to add a contact to the Contacts table.
    /// </summary>
    /// <param name="contactModel">Contact to add.</param>
    /// <returns>True if the data was inserted to the database.
    /// False if the data wasn't valid or if an exception was thrown.</returns>
    public async Task<bool> AddContactAsync(ContactModel contactModel)
    {
        try
        {
            if (!_validator.ValidatePassword(contactModel.Password))
                return false;
            Contact? contact = await CreateContactFromContactModel(contactModel);
            if (contact != null)
            {
                contact.PasswordHash = BCrypt.Net.BCrypt.EnhancedHashPassword(contactModel.Password);
                if (_validator.Validate(contact))
                {
                    await _dbContext.Contacts.AddAsync(contact);
                    await _dbContext.SaveChangesAsync();
                    return true;
                }
                else
                {
                    return false;
                }
            }
            return false;
        }
        catch
        {
            return false;
        }
    }
    /// <summary>
    /// Tries to delete a contact from the Contacts table.
    /// </summary>
    /// <param name="id">Id of the contact to delete.</param>
    /// <returns>True if the data was deleted from the database.
    /// False if id isn't valid or if an exception was thrown.</returns>
    public async Task<bool> DeleteContactAsync(int id)
    {
        try
        {
            Contact? contact = await _dbContext.Contacts.FindAsync(id);
            if (contact != null)
            {
                _dbContext.Contacts.Remove(contact);
                await _dbContext.SaveChangesAsync();
                return true;
            }
            else
            {
                return false;
            }
        }
        catch
        {
            return false;
        }
    }
    /// <summary>
    /// Joins Contacts table with People, Categories and Subcategories tables to return the contact.
    /// </summary>
    /// <param name="id">Id of the contact.</param>
    /// <returns>Contact with the given id, or null if the id isn't valid or the an exception is thrown.</returns>
    public async Task<Contact?> GetContactAsync(int id)
    {
        try
        {
            return await _dbContext.Contacts
                .Include(p => p.Person)
                .Include(c => c.Category)
                .Include(sc => sc.Subcategory)
                .AsSplitQuery()
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.Id == id);
        }
        catch
        {
            return null;
        }
    }
    /// <summary>
    /// Joins Contacts table with People, Categories and Subcategories tables to return every contact.
    /// </summary>
    /// <returns>All the contacts.</returns>
    public async Task<IEnumerable<Contact>> GetContactsAsync()
    {
        return await _dbContext.Contacts
            .Include(p => p.Person)
            .Include(c => c.Category)
            .Include(sc => sc.Subcategory)
            .AsSplitQuery()
            .AsNoTracking()
            .ToListAsync();
    }
    /// <summary>
    /// Tries to update contact with the given id.
    /// </summary>
    /// <param name="id">Id of the contact.</param>
    /// <param name="contactModel">New values.</param>
    /// <returns>True if update was successful. False if the id wasn't valid or of the model wasn't valid or
    /// if an exception was thrown.</returns>
    public async Task<bool> UpdateContactAsync(int id, ContactModel contactModel)
    {
        try
        {
            if (!_validator.ValidatePassword(contactModel.Password))
                return false;
            Contact? contactToUpdate = await _dbContext.Contacts.FindAsync(id);
            if (contactToUpdate != null)
            {
                Contact? updated = await CreateContactFromContactModel(contactModel);
                if (updated != null)
                {
                    updated.PasswordHash = BCrypt.Net.BCrypt.EnhancedHashPassword(contactModel.Password);
                    updated.Id = contactToUpdate.Id;
                    if (_validator.Validate(updated))
                    {
                        _dbContext.Contacts.Update(updated);
                        await _dbContext.SaveChangesAsync();
                        return true;
                    }
                }
            }
            return false;
        }
        catch
        {
            return false;
        }
    }
    /// <summary>
    /// Creates a contact database model from the ContactModel that's provided in the http request.
    /// </summary>
    /// <param name="contactModel">Model provided in the http request.</param>
    /// <returns>Contact if the data provided is valid, otherwise null.</returns>
    private async Task<Contact?> CreateContactFromContactModel(ContactModel contactModel)
    {
        try
        {
            Person? person = await _dbContext.People.FirstOrDefaultAsync(x => x.Id == contactModel.PersonId);
            Category? category = await _dbContext.Categories.FirstOrDefaultAsync(x => x.Id == contactModel.CategoryId);
            Subcategory? subcategory = await _dbContext.Subcategories.FirstOrDefaultAsync(x => x.Id == contactModel.SubcategoryID);
            return new()
            {
                Person = person,
                Email = contactModel.Email,
                PhoneNumber = contactModel.PhoneNumber,
                Category = category,
                Subcategory = subcategory,
            };
        }
        catch
        {
            return null;
        }
    }
}
