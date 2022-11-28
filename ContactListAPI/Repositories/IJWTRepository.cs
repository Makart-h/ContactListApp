using ContactListAPI.Models;

namespace ContactListAPI.Repositories;

public interface IJWTRepository
{
    Task<string> Authenticate(UserModel user);
}
