using ContactListAPI.Models;
using ContactListAPI.Repositories.Data.Interfaces;
using ContactListAPI.Validation.Interfaces;
using EFCoreDataAccess.Data;
using EFCoreDataAccess.Models;
using Microsoft.EntityFrameworkCore;

namespace ContactListAPI.Repositories.Data.Implementations;
/// <summary>
/// Class responsible for comunication with the People table.
/// </summary>
public class PersonRepository : IPersonRepository
{
    private readonly ContactListContext _dbContext;
    private readonly IPersonValidator _validator;

    public PersonRepository(ContactListContext dbContext, IPersonValidator validator)
    {
        _dbContext = dbContext;
        _validator = validator;
    }
    /// <summary>
    /// Tries to add a person to the People table.
    /// </summary>
    /// <param name="personModel">Person to add.</param>
    /// <returns>True if the data was inserted to the database.
    /// False if the data wasn't valid or if an exception was thrown.</returns>
    public async Task<bool> AddPersonAsync(PersonModel personModel)
    {
        try
        {
            Person person = new()
            {
                FirstName = personModel.FirstName,
                LastName = personModel.LastName,
                Birthdate = personModel.Birthdate
            };
            if (_validator.Validate(person))
            {
                await _dbContext.People.AddAsync(person);
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
    /// Tries to delete a person from the People table.
    /// </summary>
    /// <param name="id">Id of the person to delete.</param>
    /// <returns>True if the data was deleted from the database.
    /// False if id isn't valid or if an exception was thrown.</returns>
    public async Task<bool> DeletePersonAsync(int id)
    {
        Person? person = await _dbContext.People.FindAsync(id);
        if (person != null)
        {
            _dbContext.People.Remove(person);
            await _dbContext.SaveChangesAsync();
            return true;
        }
        else
        {
            return false;
        }
    }
    /// <summary>
    /// 
    /// </summary>
    /// <returns>All the rows from the People table.</returns>
    public async Task<IEnumerable<Person>> GetPeopleAsync() => await _dbContext.People.AsNoTracking().ToListAsync();

    /// <summary>
    /// 
    /// </summary>
    /// <param name="id">Id of the person.</param>
    /// <returns>Person model if the id was valid, otherwise null.</returns>
    public async Task<Person?> GetPersonAsync(int id) => await _dbContext.People.FindAsync(id);

    /// <summary>
    /// Tries to update person with the given id.
    /// </summary>
    /// <param name="id">Id of the person.</param>
    /// <param name="personModel">New values.</param>
    /// <returns>True if update was successful. False if the id wasn't valid or of the model wasn't valid or
    /// if an exception was thrown.</returns>
    public async Task<bool> UpdatePersonAsync(int id, PersonModel personModel)
    {
        try
        {
            Person? personToUpdate = await _dbContext.People.FindAsync(id);
            if (personToUpdate != null)
            {
                personToUpdate.FirstName = personModel.FirstName;
                personToUpdate.LastName = personModel.LastName;
                personToUpdate.Birthdate = personModel.Birthdate;
                if (_validator.Validate(personToUpdate))
                {
                    _dbContext.People.Update(personToUpdate);
                    await _dbContext.SaveChangesAsync();
                    return true;
                }
            }
            return false;
        }
        catch
        {
            return false;
        }
    }
}
