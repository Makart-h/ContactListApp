using ContactListAPI.Models;
using ContactListAPI.Repositories;
using ContactListAPI.Repositories.Data.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ContactListAPI.Controllers;
/// <summary>
/// Controller for Users requests with some requests available without JWT.
/// </summary>
[Route("api/[controller]")]
[ApiController]
[Authorize]
public class UsersController : ControllerBase
{
	private readonly IJWTRepository _jwtRepository;

	public UsersController(IJWTRepository jwtRepository)
	{
		_jwtRepository = jwtRepository;
	}

    // POST api/<UsersController>
    [AllowAnonymous]
	[HttpPost]
	public async Task<ActionResult<string>> Login([FromBody] UserModel user)
	{
		string token = await _jwtRepository.Authenticate(user);
		if (token == string.Empty)
			return BadRequest("Not a valid user data!");
		else
			return Ok(token);
	}
}
