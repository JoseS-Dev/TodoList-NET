using TodoList_NET.Services.CategoryServices;
using TodoList_NET.Models.Categories;
using TodoList_NET.Dtos;
using TodoList_NET.Data;

// Defino la clase del servicio de las categorias
public class CategoryService : ICategoryService
{
    private readonly ApplicationDbContext _context;

    public CategoryService(ApplicationDbContext context)
    {
        _context = context;
    }

    // Método para obtener todas las categorias
    public async Task<List<Category>> GetAllCategoriesAsync(DtoParam param)
    {
        var query = _context.Categories.AsQueryable();

        // Si se proporciona un parámetro de búsqueda, se filtran las categorias por nombre o descripcion
        if (!string.IsNullOrEmpty(param.Search))
        {
            query = query.Where(c => c.NameCategory.Contains(param.Search) || c.DescriptionCategory!.Contains(param.Search));
        }

        // Se aplica la paginación
        var categories = await query.Skip((param.Page - 1) * param.Limit).Take(param.Limit).ToListAsync();
        return categories;
    }

    // Método para obtener una categoria por su ID
    public async Task<Category?> GetCategoryByIdAsync(int id)
    {
        return await _context.Categories.FindAsync(id);
    }

    // Método para crear una nueva categoria
    public async Task<DtoCategoryResponse> CreateCategoryAsync(DtoCategoryCreate category)
    {
        // Se valida que no exista una categoria con el mismo nombre
        if(await _context.Categories.AnyAsync(c => c.NameCategory == category.NameCategory))
        {
            throw new Exception("La categoria ya existe");
        }
        // Si no existe se crea la categoria
        var newCategory = new Category
        {
            NameCategory = category.NameCategory,
            DescriptionCategory = category.DescriptionCategory
        };
        _context.Categories.Add(newCategory);
        await _context.SaveChangesAsync();
        var categoryResponse = new DtoCategoryResponse
        {
            Id = newCategory.Id,
            NameCategory = newCategory.NameCategory,
            DescriptionCategory = newCategory.DescriptionCategory
        };
        return categoryResponse;
    }

    // Método para actualizar una categoria
    public async Task<DtoCategoryResponse?> UpdateCategoryAsync(int id, DtoCategoryUpdate category)
    {
        var existingCategory = await _context.Categories.FindAsync(id);
        if (existingCategory == null)
        {
            return null;
        }
        // Si se va a actualizar el nombre de la categoria, se valida que no exista otra categoria con el mismo nombre
        if (!string.IsNullOrEmpty(category.NameCategory) && await _context.Categories.AnyAsync(c => c.NameCategory == category.NameCategory && c.Id != id))
        {
            throw new Exception("La categoria ya existe");
        }
        // Se actualizan los campos de la categoria
        existingCategory.NameCategory = category.NameCategory ?? existingCategory.NameCategory;
        existingCategory.DescriptionCategory = category.DescriptionCategory ?? existingCategory.DescriptionCategory;
        existingCategory.UpdatedAt = DateTime.Now;
        await _context.SaveChangesAsync();
        var categoryResponse = new DtoCategoryResponse
        {
            Id = existingCategory.Id,
            NameCategory = existingCategory.NameCategory,
            DescriptionCategory = existingCategory.DescriptionCategory
        };
        return categoryResponse;
    }

    // Método para eliminar una categoria
    public async Task<bool> DeleteCategoryAsync(int id)
    {
        var existingCategory = await _context.Categories.FindAsync(id);
        if (existingCategory == null)
        {
            return false;
        }
        existingCategory.DeletedAt = DateTime.Now;
        await _context.SaveChangesAsync();
        return true;
    }
}