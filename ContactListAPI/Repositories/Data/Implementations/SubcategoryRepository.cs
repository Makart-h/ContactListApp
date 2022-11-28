using ContactListAPI.Models;
using ContactListAPI.Repositories.Data.Interfaces;
using ContactListAPI.Validation.Interfaces;
using EFCoreDataAccess.Data;
using EFCoreDataAccess.Models;
using Microsoft.EntityFrameworkCore;

namespace ContactListAPI.Repositories.Data.Implementations;
/// <summary>
/// Class responsible for comunication with the Subcategories table.
/// </summary>
public class SubcategoryRepository : ISubcategoryRepository
{
    private readonly ContactListContext _dbContext;
    private readonly ISubcategoryValidator _validator;

    public SubcategoryRepository(ContactListContext dbContext, ISubcategoryValidator validator)
    {
        _dbContext = dbContext;
        _validator = validator;
    }
    /// <summary>
    /// Tries to add a subcategory to the Subcategories table.
    /// </summary>
    /// <param name="subcategoryModel">Subcategory to add.</param>
    /// <returns>Added subcategory if the data was inserted to the database.
    /// Null if the data wasn't valid or if an exception was thrown.</returns>
    public async Task<Subcategory?> AddSubcategoryAsync(SubcategoryModel subcategoryModel)
    {
        try
        {
            Subcategory subcategory = new()
            {
                Name = subcategoryModel.Name
            };
            if (_validator.Validate(subcategory))
            {
                await _dbContext.Subcategories.AddAsync(subcategory);
                await _dbContext.SaveChangesAsync();
                return subcategory;
            }
            else
            {
                return null;
            }
        }
        catch
        {
            return null;
        }
    }
    /// <summary>
    /// Tries to delete a subcategory from the Subcategories table.
    /// </summary>
    /// <param name="id">Id of the subcategory to delete.</param>
    /// <returns>True if the data was deleted from the database.
    /// False if id isn't valid or if an exception was thrown.</returns>
    public async Task<bool> DeleteSubcategoryAsync(int id)
    {
        try
        {
            Subcategory? subcategory = await _dbContext.Subcategories.FindAsync(id);
            if (subcategory != null)
            {
                _dbContext.Subcategories.Remove(subcategory);
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
    /// 
    /// </summary>
    /// <returns>All the rows from the Subcategories table.</returns>
    public async Task<IEnumerable<Subcategory>> GetSubcategoriesAsync() => await _dbContext.Subcategories.AsNoTracking().ToListAsync();
    
    /// <summary>
    /// 
    /// </summary>
    /// <param name="id">Id of the subcategory.</param>
    /// <returns>Subcategory model if the id was valid, otherwise null.</returns>
    public async Task<Subcategory?> GetSubcategoryAsync(int id) => await _dbContext.Subcategories.FindAsync(id);

    /// <summary>
    /// Tries to update subcategory with the given id.
    /// </summary>
    /// <param name="id">Id of the subcategory.</param>
    /// <param name="subcategoryModel">New values.</param>
    /// <returns>True if update was successful. False if the id wasn't valid or if an exception was thrown.</returns>
    public async Task<bool> UpdateSubcategoryAsync(int id, SubcategoryModel subcategoryModel)
    {
        try
        {
            Subcategory? subcategoryToUpdate = await _dbContext.Subcategories.FindAsync(id);
            if (subcategoryToUpdate != null)
            {
                subcategoryToUpdate.Name = subcategoryModel.Name;
                if (_validator.Validate(subcategoryToUpdate))
                {
                    _dbContext.Subcategories.Update(subcategoryToUpdate);
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
