using ContactListAPI.Models;
using EFCoreDataAccess.Models;

namespace ContactListAPI.Repositories.Data.Interfaces;
/// <summary>
/// Interface responsible for comunication with the People table.
/// </summary>
public interface IPersonRepository
{
    Task<IEnumerable<Person>> GetPeopleAsync();
    Task<Person?> GetPersonAsync(int id);
    Task<bool> AddPersonAsync(PersonModel person);
    Task<bool> UpdatePersonAsync(int id, PersonModel person);
    Task<bool> DeletePersonAsync(int id);
}
