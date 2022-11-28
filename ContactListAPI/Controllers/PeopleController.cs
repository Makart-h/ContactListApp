using ContactListAPI.Models;
using ContactListAPI.Repositories.Data.Interfaces;
using EFCoreDataAccess.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ContactListAPI.Controllers;
/// <summary>
/// Controller for People requests available only with JWT.
/// </summary>
[Route("api/[controller]")]
[ApiController]
[Authorize]
public class PeopleController : ControllerBase
{
    private readonly IPersonRepository _personRepository;

    public PeopleController(IPersonRepository personRepository)
    {
        _personRepository = personRepository;
    }

    // GET: api/<PeopleController>
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Person>>> Get()
    {
        return Ok(await _personRepository.GetPeopleAsync());
    }

    // GET api/<PeopleController>/5
    [HttpGet("{id}")]
    public async Task<ActionResult<Person>> Get(int id)
    {
        Person? person = await _personRepository.GetPersonAsync(id);
        if (person == null)
            return NotFound(id);
        else
            return Ok(person);
    }

    // POST api/<PeopleController>
    [HttpPost]
    public async Task<ActionResult<PersonModel>> Post([FromBody] PersonModel person)
    {
        bool success = await _personRepository.AddPersonAsync(person);
        if (success)
            return Ok(person);
        else
            return BadRequest(person);
    }

    // PUT api/<PeopleController>/5
    [HttpPut("{id}")]
    public async Task<ActionResult<int>> Put(int id, [FromBody] PersonModel person)
    {
        bool success = await _personRepository.UpdatePersonAsync(id, person);
        if (success)
            return Ok(id);
        else
            return NotFound(id);
    }

    // DELETE api/<PeopleController>/5
    [HttpDelete("{id}")]
    public async Task<ActionResult<int>> Delete(int id)
    {
        bool success = await _personRepository.DeletePersonAsync(id);
        if (success)
            return Ok(id);
        else
            return NotFound(id);
    }
}
