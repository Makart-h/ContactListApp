using ContactListAPI.Models;
using ContactListAPI.Repositories.Data.Interfaces;
using EFCoreDataAccess.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ContactListAPI.Controllers;
/// <summary>
/// Controller for Categories requests available only with JWT.
/// </summary>
[Route("api/[controller]")]
[ApiController]
[Authorize]
public class CategoriesController : ControllerBase
{
    private readonly ICategoryRepository _categoryRepository;

    public CategoriesController(ICategoryRepository categoryRepository)
    {
        _categoryRepository = categoryRepository;
    }

    // GET: api/<CategoriesController>  
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Category>>> Get()
    {
        return Ok(await _categoryRepository.GetCategoriesAsync());
    }

    // GET api/<CategoriesController>/5
    [HttpGet("{id}")]
    public async Task<ActionResult<Category>> Get(int id)
    {
        Category? category = await _categoryRepository.GetCategoryAsync(id);
        if (category == null)
            return NotFound(id);
        else
            return Ok(category);
    }

    // POST api/<CategoriesController>
    [HttpPost]
    public async Task<ActionResult<CategoryModel>> Post([FromBody] CategoryModel category)
    {
        bool success = await _categoryRepository.AddCategoryAsync(category);
        if (success)
            return Ok(category);
        else
            return BadRequest(category);
    }

    // PUT api/<CategoriesController>/5
    [HttpPut("{id}")]
    public async Task<ActionResult<int>> Put(int id, [FromBody] CategoryModel category)
    {
        bool success = await _categoryRepository.UpdateCategoryAsync(id, category);
        if (success)
            return Ok(id);
        else
            return NotFound(id);
    }

    // DELETE api/<CategoriesController>/5
    [HttpDelete("{id}")]
    public async Task<ActionResult<int>> Delete(int id)
    {
        bool success = await _categoryRepository.DeleteCategoryAsync(id);
        if (success)
            return Ok(id);
        else
            return NotFound(id);
    }
}
