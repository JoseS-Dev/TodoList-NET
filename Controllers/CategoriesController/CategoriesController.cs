using TodoList_NET.Services.CategoryServices;
using TodoList_NET.Models.Categories;
using TodoList_NET.Dtos;

namespace TodoList_NET.Controllers.CategoriesController;

[ApiController]
[Route("api/[controller]")]
public class CategoriesController : ControllerBase
{
    private readonly ICategoryService _categoryService;

    public CategoriesController(ICategoryService categoryService)
    {
        _categoryService = categoryService;
    }

    [HttpGet]
    public async Task<ActionResult<List<Category>>> GetAllCategories(
        [FromQuery] DtoParam param
    )
    {
        var categories = await _categoryService.GetAllCategoriesAsync(param);
        return Ok(categories);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Category?>> GetCategoryById(int id)
    {
        var category = await _categoryService.GetCategoryByIdAsync(id);
        if (category == null)
        {
            return NotFound();
        }
        return Ok(category);
    }

    [HttpPost]
    public async Task<ActionResult<DtoCategoryCreate>> CreateCategory(DtoCategoryCreate category)
    {
        var createdCategory = await _categoryService.CreateCategoryAsync(category);
        return CreatedAtAction(nameof(GetCategoryById), new { id = createdCategory.Id }, createdCategory);
    }

    [HttpPatch("{id}")]
    public async Task<ActionResult<DtoCategoryUpdate?>> UpdateCategory(int id, DtoCategoryUpdate category)
    {
        var updatedCategory = await _categoryService.UpdateCategoryAsync(id, category);
        if (updatedCategory == null)
        {
            return NotFound();
        }
        return Ok(updatedCategory);
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteCategory(int id)
    {
        var deleted = await _categoryService.DeleteCategoryAsync(id);
        if (!deleted)
        {
            return NotFound();
        }
        return NoContent();
    }
}