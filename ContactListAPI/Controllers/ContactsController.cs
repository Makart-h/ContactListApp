using ContactListAPI.Models;
using ContactListAPI.Repositories.Data.Interfaces;
using EFCoreDataAccess.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ContactListAPI.Controllers;
/// <summary>
/// Controller for Contacts requests available only with JWT.
/// </summary>
[Route("api/[controller]")]
[ApiController]
[Authorize]
public class ContactsController : ControllerBase
{
    private readonly IContactRepository _contactRepository;

    public ContactsController(IContactRepository contactRepository)
    {
        _contactRepository = contactRepository;
    }

    // GET: api/<ContactsController>
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Contact>>> Get()
    {
        return Ok(await _contactRepository.GetContactsAsync());
    }

    // GET api/<ContactsController>/5
    [HttpGet("{id}")]
    public async Task<ActionResult<Contact>> Get(int id)
    {
        Contact? contact = await _contactRepository.GetContactAsync(id);
        if (contact == null)
            return NotFound(id);
        else
            return Ok(contact);
    }

    // POST api/<ContactsController>
    [HttpPost]
    public async Task<ActionResult<ContactModel>> Post([FromBody] ContactModel contact)
    {
        bool success = await _contactRepository.AddContactAsync(contact);
        if (success)
            return Ok(contact);
        else
            return BadRequest(contact);
    }

    // PUT api/<ContactsController>/5
    [HttpPut("{id}")]
    public async Task<ActionResult<int>> Put(int id, [FromBody] ContactModel contact)
    {
        bool success = await _contactRepository.UpdateContactAsync(id, contact);
        if (success)
            return Ok(id);
        else
            return NotFound(id);
    }

    // DELETE api/<ContactsController>/5
    [HttpDelete("{id}")]
    public async Task<ActionResult<int>> Delete(int id)
    {
        bool success = await _contactRepository.DeleteContactAsync(id);
        if (success)
            return Ok(id);
        else
            return NotFound(id);
    }
}
