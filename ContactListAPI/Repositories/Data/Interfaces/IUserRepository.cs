using EFCoreDataAccess.Models;

namespace ContactListAPI.Repositories.Data.Interfaces;
/// <summary>
/// Interface responsible for comunication with the Users table.
/// </summary>
public interface IUserRepository
{
    Task<User?> GetUserAsync(string username);
}
