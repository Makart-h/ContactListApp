using ContactListAPI.Repositories.Data.Interfaces;
using EFCoreDataAccess.Data;
using EFCoreDataAccess.Models;
using Microsoft.EntityFrameworkCore;

namespace ContactListAPI.Repositories.Data.Implementations;
/// <summary>
/// Class responsible for comunication with the Users table.
/// </summary>
public class UserRepository : IUserRepository
{
    private readonly ContactListContext _dbContext;

    public UserRepository(ContactListContext dbContext)
    {
        _dbContext = dbContext;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="username">Username of the user.</param>
    /// <returns>User model if the username was valid, otherwise null.</returns>
    public async Task<User?> GetUserAsync(string username) => await _dbContext.Users.FirstOrDefaultAsync(x => x.Username == username);
}
