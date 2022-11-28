using ContactListAPI.Models;
using EFCoreDataAccess.Models;

namespace ContactListAPI.Repositories.Data.Interfaces;
/// <summary>
/// Interface responsible for comunication with the Contacts table.
/// </summary>
public interface IContactRepository
{
    Task<IEnumerable<Contact>> GetContactsAsync();
    Task<Contact?> GetContactAsync(int id);
    Task<bool> AddContactAsync(ContactModel contact);
    Task<bool> UpdateContactAsync(int id, ContactModel contact);
    Task<bool> DeleteContactAsync(int id);
}
