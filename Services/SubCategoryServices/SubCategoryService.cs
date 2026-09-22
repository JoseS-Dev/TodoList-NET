using TodoList_NET.Dtos;
using TodoList_NET.Data;
using TodoList_NET.Models.SubCategories;
using TodoList_NET.Services.SubCategoryServices;

// Defino el servicio de las subcategorias
public class SubCategoryService : ISubCategoryService
{
    private readonly ApplicationDbContext _context;

    public SubCategoryService(ApplicationDbContext context)
    {
        _context = context;
    }

    // Método para obtener todas las subcategorias con paginación
    public async Task<List<SubCategory>> GetAllSubCategoriesAsync(DtoParam param)
    {
        var query = _context.SubCategories.AsQueryable();

        // Si se proporciona un parámetro de búsqueda, se filtran las subcategorias por nombre o descripcion
        if(!string.IsNullOrEmpty(param.Search))
        {
            query = query.Where(sc => sc.NameSubCategory.Contains(param.Search) || sc.DescriptionSubCategory!.Contains(param.Search));
        }
        // Se aplica la paginación
        var subcategories = await query
        .Where(sc => sc.DeletedAt == null)
        .Skip((param.Page - 1) * param.Limit)
        .Take(param.Limit)
        .ToListAsync();

        return subcategories;
    }

    // Método para obtener todas las subcategorias de una categoria
    public async Task<List<SubCategory>> GetSubCategoriesByCategoryIdAsync(int categoryId, DtoParam param)
    {
        // Se verifica que exista la categoria en cuestión
        var existingCategory = await _context.Categories.FindAsync(categoryId);
        if (existingCategory == null)
        {
            throw new Exception("La categoria no existe");
        }
        // Si existe, se obtienen las subcategorias de la categoria
        var query = _context.SubCategories.AsQueryable();
        // Si se proporciona un parámetro de búsqueda, se filtran las subcategorias por
        if(!string.IsNullOrEmpty(param.Search))
        {
            query = query.Where(sc => sc.NameSubCategory.Contains(param.Search) || sc.DescriptionSubCategory!.Contains(param.Search));
        }
        var subcategories = await query
        .Where(sc => sc.CategoryId == categoryId && sc.DeletedAt == null)
        .Skip((param.Page - 1) * param.Limit)
        .Take(param.Limit)
        .ToListAsync();

        return subcategories;
    }

    // Método para obtener una subcategoria por su ID
    public async Task<SubCategory?> GetSubCategoryByIdAsync(int id)
    {
        return await _context.SubCategories
        .Where(sc => sc.DeletedAt == null)
        .FirstOrDefaultAsync(sc => sc.Id == id);
    }

    // Método para crear una nueva subcategoria
    public async Task<DtoSubCategoryResponse> CreateSubCategoryAsync(DtoSubCategoryCreate subCategory)
    {
        // Se valida que exista la categoria a la que pertenece la subcategoria
        var existingCategory = await _context.Categories.FindAsync(subCategory.CategoryId);
        if (existingCategory == null)
        {
            throw new Exception("La categoria no existe");
        }
        // Se valida que no exista una subcategoria con el mismo nombre en la misma categoria
        if(await _context.SubCategories.AnyAsync(sc => sc.NameSubCategory == subCategory.NameSubCategory && sc.CategoryId == subCategory.CategoryId))
        {
            throw new Exception("La subcategoria ya existe");
        }
        // Si no existe se crea la subcategoria
        var newSubCategory = new SubCategory
        {
            CategoryId = subCategory.CategoryId,
            NameSubCategory = subCategory.NameSubCategory,
            DescriptionSubCategory = subCategory.DescriptionSubCategory
        };
        _context.SubCategories.Add(newSubCategory);
        await _context.SaveChangesAsync();
        return new DtoSubCategoryResponse
        {
            Id = newSubCategory.Id,
            CategoryId = newSubCategory.CategoryId,
            NameSubCategory = newSubCategory.NameSubCategory,
            DescriptionSubCategory = newSubCategory.DescriptionSubCategory
        };
    }

    // Método para actualizar una subcategoria
    public async Task<DtoSubCategoryResponse?> UpdateSubCategoryAsync(int id, DtoSubCategoryUpdate subCategory)
    {
        var existingSubCategory = await _context.SubCategories.FindAsync(id);
        if (existingSubCategory == null)
        {
            return null;
        }
        // Se valida que exista la categoria a la que pertenece la subcategoria
        var existingCategory = await _context.Categories.FindAsync(subCategory.CategoryId);
        if (existingCategory == null)
        {
            throw new Exception("La categoria no existe");
        }
        // Se valida que no exista una subcategoria con el mismo nombre en la misma categoria
        if(await _context.SubCategories.AnyAsync(sc => sc.NameSubCategory == subCategory.NameSubCategory && sc.CategoryId == subCategory.CategoryId && sc.Id != id))
        {
            throw new Exception("La subcategoria ya existe");
        }
        // Si no existe se actualiza la subcategoria
        existingSubCategory.CategoryId = subCategory.CategoryId;
        existingSubCategory.NameSubCategory = subCategory.NameSubCategory!;
        existingSubCategory.DescriptionSubCategory = subCategory.DescriptionSubCategory!;
        existingSubCategory.UpdatedAt = DateTime.UtcNow;

        await _context.SaveChangesAsync();
        return new DtoSubCategoryResponse
        {
            Id = existingSubCategory.Id,
            CategoryId = existingSubCategory.CategoryId,
            NameSubCategory = existingSubCategory.NameSubCategory,
            DescriptionSubCategory = existingSubCategory.DescriptionSubCategory
        };
    }

    // Método para eliminar una subcategoria
    public async Task<bool> DeleteSubCategoryAsync(int id)
    {
        var existingSubCategory = await _context.SubCategories.FindAsync(id);
        if (existingSubCategory == null)
        {
            return false;
        }
        // Se elimina la subcategoria
        existingSubCategory.DeletedAt = DateTime.UtcNow;
        await _context.SaveChangesAsync();
        return true;
    }


}