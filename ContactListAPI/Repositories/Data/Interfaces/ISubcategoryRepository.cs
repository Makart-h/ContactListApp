using ContactListAPI.Models;
using EFCoreDataAccess.Models;

namespace ContactListAPI.Repositories.Data.Interfaces;
/// <summary>
/// Interface responsible for comunication with the Subcategories table.
/// </summary>
public interface ISubcategoryRepository
{
    Task<IEnumerable<Subcategory>> GetSubcategoriesAsync();
    Task<Subcategory?> GetSubcategoryAsync(int id);
    Task<Subcategory?> AddSubcategoryAsync(SubcategoryModel subcategory);
    Task<bool> UpdateSubcategoryAsync(int id, SubcategoryModel subcategory);
    Task<bool> DeleteSubcategoryAsync(int id);
}
