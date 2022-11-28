using ContactListAPI.Models;
using ContactListAPI.Repositories.Data.Interfaces;
using EFCoreDataAccess.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ContactListAPI.Controllers;
/// <summary>
/// Controller for Subcategories requests available only with JWT.
/// </summary>
[Route("api/[controller]")]
[ApiController]
[Authorize]
public class SubcategoriesController : ControllerBase
{
    private readonly ISubcategoryRepository _subcategoryRepository;

    public SubcategoriesController(ISubcategoryRepository subcategoryRepository)
    {
        _subcategoryRepository = subcategoryRepository;
    }

    // GET: api/<SubcategoriesController>
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Subcategory>>> Get()
    {
        return Ok(await _subcategoryRepository.GetSubcategoriesAsync());
    }

    // GET api/<SubcategoriesController>/5
    [HttpGet("{id}")]
    public async Task<ActionResult<Subcategory>> Get(int id)
    {
        Subcategory? subcategory = await _subcategoryRepository.GetSubcategoryAsync(id);
        if (subcategory == null)
            return NotFound(id);
        else
            return Ok(subcategory);
    }

    // POST api/<SubcategoriesController>
    [HttpPost]
    public async Task<ActionResult<Subcategory>> Post([FromBody] SubcategoryModel subcategory)
    {
        Subcategory? sub = await _subcategoryRepository.AddSubcategoryAsync(subcategory);
        if (sub != null)
            return Ok(sub);
        else
            return BadRequest(sub);
    }

    // PUT api/<SubcategoriesController>/5
    [HttpPut("{id}")]
    public async Task<ActionResult<int>> Put(int id, [FromBody] SubcategoryModel subcategory)
    {
        bool success = await _subcategoryRepository.UpdateSubcategoryAsync(id, subcategory);
        if (success)
            return Ok(id);
        else
            return NotFound(id);
    }

    // DELETE api/<SubcategoriesController>/5
    [HttpDelete("{id}")]
    public async Task<ActionResult<int>> Delete(int id)
    {
        bool success = await _subcategoryRepository.DeleteSubcategoryAsync(id);
        if (success)
            return Ok(id);
        else
            return NotFound(id);
    }
}
