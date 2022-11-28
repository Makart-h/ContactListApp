using ContactListAPI.Models;
using EFCoreDataAccess.Models;

namespace ContactListAPI.Repositories.Data.Interfaces;
/// <summary>
/// Interface responsible for comunication with the Categories table.
/// </summary>
public interface ICategoryRepository
{
    Task<IEnumerable<Category>> GetCategoriesAsync();
    Task<Category?> GetCategoryAsync(int id);
    Task<bool> AddCategoryAsync(CategoryModel category);
    Task<bool> UpdateCategoryAsync(int id, CategoryModel category);
    Task<bool> DeleteCategoryAsync(int id);
}
