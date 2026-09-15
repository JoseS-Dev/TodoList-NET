// Defino la interfaz para los metodos del servicio de las categorias
using TodoList_NET.Dtos;
using TodoList_NET.Models.Categories;

namespace TodoList_NET.Services.CategoryServices;

public interface ICategoryService
{
    Task<List<Category>> GetAllCategoriesAsync(DtoParam param);
    Task<Category?> GetCategoryByIdAsync(int id);
    Task<DtoCategoryResponse> CreateCategoryAsync(DtoCategoryCreate category);
    Task<DtoCategoryResponse?> UpdateCategoryAsync(int id, DtoCategoryUpdate category);
    Task<bool> DeleteCategoryAsync(int id);
}