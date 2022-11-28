using ContactListAPI.Models;
using ContactListAPI.Repositories.Data.Interfaces;
using ContactListAPI.Validation.Interfaces;
using EFCoreDataAccess.Data;
using EFCoreDataAccess.Models;
using Microsoft.EntityFrameworkCore;

namespace ContactListAPI.Repositories.Data.Implementations;
/// <summary>
/// Class responsible for comunication with the Categories table.
/// </summary>
public class CategoryRepository : ICategoryRepository
{
    private readonly ContactListContext _dbContext;
    private readonly ICategoryValidator _validator;

    public CategoryRepository(ContactListContext dbContext, ICategoryValidator validator)
    {
        _dbContext = dbContext;
        _validator = validator;
    }
    /// <summary>
    /// Tries to add a category to the Categories table.
    /// </summary>
    /// <param name="categoryModel">Category to add.</param>
    /// <returns>True if the data was inserted to the database.
    /// False if the data wasn't valid or if an exception was thrown.</returns>
    public async Task<bool> AddCategoryAsync(CategoryModel categoryModel)
    {
        try
        {
            Category category = new() { Name = categoryModel.Name };
            if (_validator.Validate(category))
            {
                await _dbContext.Categories.AddAsync(category);
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
    /// Tries to delete a category from the Categories table.
    /// </summary>
    /// <param name="id">Id of the category to delete.</param>
    /// <returns>True if the data was deleted from the database.
    /// False if id isn't valid or if an exception was thrown.</returns>
    public async Task<bool> DeleteCategoryAsync(int id)
    {
        try
        {
            Category? category = await _dbContext.Categories.FindAsync(id);
            if (category != null)
            {
                _dbContext.Categories.Remove(category);
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
    /// <returns>All the rows from the Categories table.</returns>
    public async Task<IEnumerable<Category>> GetCategoriesAsync() => await _dbContext.Categories.AsNoTracking().ToListAsync();
    /// <summary>
    /// 
    /// </summary>
    /// <param name="id">Id of the category.</param>
    /// <returns>Category model if the id was valid, otherwise null.</returns>
    public async Task<Category?> GetCategoryAsync(int id) => await _dbContext.Categories.FindAsync(id);
    /// <summary>
    /// Tries to update category with the given id.
    /// </summary>
    /// <param name="id">Id of the category.</param>
    /// <param name="categoryModel">New values.</param>
    /// <returns>True if update was successful. False if the id wasn't valid or if an exception was thrown.</returns>
    public async Task<bool> UpdateCategoryAsync(int id, CategoryModel categoryModel)
    {
        try
        {
            Category? categoryToUpdate = await _dbContext.Categories.FindAsync(id);
            if (categoryToUpdate != null)
            {
                categoryToUpdate.Name = categoryModel.Name;
                if (_validator.Validate(categoryToUpdate))
                {
                    _dbContext.Categories.Update(categoryToUpdate);
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
