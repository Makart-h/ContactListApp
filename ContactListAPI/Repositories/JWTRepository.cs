using ContactListAPI.Models;
using ContactListAPI.Repositories.Data.Interfaces;
using EFCoreDataAccess.Models;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace ContactListAPI.Repositories;
/// <summary>
/// Class responsible for authenticating users and providing JWT.
/// </summary>
public class JWTRepository : IJWTRepository
{
    private readonly IUserRepository _userRepository;
    private readonly IConfiguration _configuration;

    public JWTRepository(IUserRepository userRepository, IConfiguration configuration)
    {
        _userRepository = userRepository;
        _configuration = configuration;
    }
    /// <summary>
    /// Checks if user exists in the database and if the hash of provided password
    /// matches the hash present in database.
    /// </summary>
    /// <param name="userModel">Model containing username and password.</param>
    /// <returns>String of JWT if authentication is successful, otherwise empty string.</returns>
    public async Task<string> Authenticate(UserModel userModel)
    {
        User? user = await _userRepository.GetUserAsync(userModel.Username);
        if (user != null && BCrypt.Net.BCrypt.EnhancedVerify(userModel.Password, user.PasswordHash))
            return GenerateToken(user);
        else
            return string.Empty;
    }
    /// <summary>
    /// Reads values of Key, Issuer and Audience
    /// from configuration and passes them to GenerateToken method.
    /// </summary>
    /// <param name="user">User read from the database.</param>
    /// <returns>String of JWT if provided arguments were valid, otherwise empty string.</returns>
    private string GenerateToken(User user)
    {
        string? jwtKey = _configuration["Jwt:Key"];
        string? issuer = _configuration["Jwt:Issuer"];
        string? audience = _configuration["Jwt:Audience"];

        if (jwtKey != null && issuer != null && audience != null)
            return GenerateToken(user, jwtKey, issuer, audience);
        else
            return string.Empty;
    }
    /// <summary>
    /// Generates JWT from the provided arguments.
    /// </summary>
    /// <param name="user">User from the database for whom the token is created.</param>
    /// <param name="jwtKey">Key that will be used to create SymmetricSecurityKey.</param>
    /// <param name="issuer">Issuer of the token.</param>
    /// <param name="audience">Audience of the token.</param>
    /// <returns>JWT written to a string if arguments were valid, otherwise empty string.</returns>
    private static string GenerateToken(User user, string jwtKey, string issuer, string audience)
    {
        Byte[] keyBytes = Encoding.UTF8.GetBytes(jwtKey);
        SymmetricSecurityKey securityKey = new(keyBytes);
        SigningCredentials credentials = new(securityKey, SecurityAlgorithms.HmacSha256);

        Claim[] claims = GetClaims(user);
        DateTime expireDate = DateTime.Now.AddMinutes(10);

        JwtSecurityToken token = new(
            issuer: issuer,
            audience: audience,
            claims: claims,
            expires: expireDate,
            signingCredentials: credentials);

        JwtSecurityTokenHandler handler = new();
        return handler.WriteToken(token);
    }
    /// <summary>
    /// Creates Claim array to by used in JWT.
    /// </summary>
    /// <param name="user">User from the database for whom the token is created.</param>
    /// <returns>Array of type Claim.</returns>
    private static Claim[] GetClaims(User user)
    {
        return new Claim[]
        {
            new Claim(ClaimTypes.Name, user.Username)
        };
    }
}
