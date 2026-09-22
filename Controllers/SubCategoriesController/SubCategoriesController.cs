using TodoList_NET.Dtos;
using TodoList_NET.Models.SubCategories;
using TodoList_NET.Services.SubCategoryServices;

namespace TodoList_NET.Controllers.SubCategoriesController;

[ApiController]
[Route("api/[controller]")]
public class SubCategoriesController : ControllerBase
{
    private readonly ISubCategoryService _subCategoryService;

    public SubCategoriesController(ISubCategoryService subCategoryService)
    {
        _subCategoryService = subCategoryService;
    }

    [HttpGet]
    public async Task<ActionResult<List<SubCategory>>> GetAllSubCategories(
        [FromQuery] DtoParam param
    )
    {
        var subCategories = await _subCategoryService.GetAllSubCategoriesAsync(param);
        return Ok(subCategories);
    }

    [HttpGet("category/{categoryId}")]
    public async Task<ActionResult<List<SubCategory>>> GetSubCategoriesByCategoryId(int categoryId, [FromQuery] DtoParam param)
    {
        var subCategories = await _subCategoryService.GetSubCategoriesByCategoryIdAsync(categoryId, param);
        return Ok(subCategories);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<SubCategory?>> GetSubCategoryById(int id)
    {
        var subCategory = await _subCategoryService.GetSubCategoryByIdAsync(id);
        if (subCategory == null)
        {
            return NotFound();
        }
        return Ok(subCategory);
    }

    [HttpPost]
    public async Task<ActionResult<DtoSubCategoryCreate>> CreateSubCategory(DtoSubCategoryCreate subCategory)
    {
        var createdSubCategory = await _subCategoryService.CreateSubCategoryAsync(subCategory);
        return CreatedAtAction(nameof(GetSubCategoryById), new { id = createdSubCategory.Id }, createdSubCategory);
    }

    [HttpPatch("{id}")]
    public async Task<ActionResult<DtoSubCategoryUpdate?>> UpdateSubCategory(int id, DtoSubCategoryUpdate subCategory)
    {
        var updatedSubCategory = await _subCategoryService.UpdateSubCategoryAsync(id, subCategory);
        if (updatedSubCategory == null)
        {
            return NotFound();
        }
        return Ok(updatedSubCategory);
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteSubCategory(int id)
    {
        var deleted = await _subCategoryService.DeleteSubCategoryAsync(id);
        if (!deleted)
        {
            return NotFound();
        }
        return NoContent();
    }
}