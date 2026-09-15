// Defino la interfaz del servicio de las subcategorias
using TodoList_NET.Dtos;
using TodoList_NET.Models.SubCategories;

namespace TodoList_NET.Services.SubCategoryServices;

public interface ISubCategoryService
{
    Task<List<SubCategory>> GetAllSubCategoriesAsync(DtoParam param);
    Task<List<SubCategory>> GetSubCategoriesByCategoryIdAsync(int categoryId, DtoParam param);
    Task<SubCategory?> GetSubCategoryByIdAsync(int id);
    Task<DtoSubCategoryResponse> CreateSubCategoryAsync(DtoSubCategoryCreate subCategory);
    Task<DtoSubCategoryResponse?> UpdateSubCategoryAsync(int id, DtoSubCategoryUpdate subCategory);
    Task<bool> DeleteSubCategoryAsync(int id);
}

