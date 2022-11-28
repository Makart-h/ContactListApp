using ContactListAPI.Models;
using ContactListAPI.Repositories.Data.Interfaces;
using EFCoreDataAccess.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ContactListAPI.Controllers;
/// <summary>
/// Controller for OpenContacts requests available without authorization.
/// </summary>
[Route("api/[controller]")]
[ApiController]
public class OpenContactsController : ControllerBase
{
    private readonly IContactRepository _contactRepository;

    public OpenContactsController(IContactRepository contactRepository)
    {
        _contactRepository = contactRepository;
    }

    // GET: api/<OpenContactsController>
    [HttpGet]
    public async Task<ActionResult<List<OpenContactModel>>> Get()
    {
        IEnumerable<Contact> contacts = await _contactRepository.GetContactsAsync();
        List<OpenContactModel> openContacts = new();
        foreach(Contact contact in contacts)
        {
            OpenContactModel model = new()
            {
                Id = contact.Id,
                FirstName = contact.Person.FirstName,
                LastName = contact.Person.LastName,
                Category = contact.Category.Name,
                Subcategory = contact.Subcategory?.Name
            };
            openContacts.Add(model);
        }
        return Ok(openContacts);
    }
}
