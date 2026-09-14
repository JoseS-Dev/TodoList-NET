using TodoList_NET.Models.Categories;

// Defino el modelo de las subcategorias
namespace TodoList_NET.Models.SubCategories;

public class SubCategory
{
    public int Id {get; set;}
    public int CategoryId {get; set;}
    public string NameSubCategory {get; set;} = string.Empty;
    public string? DescriptionSubCategory {get; set;} = string.Empty;
    public DateTime CreatedAt {get; set;} = DateTime.Now;
    public DateTime UpdatedAt {get; set;} = DateTime.Now;
    public DateTime? DeletedAt {get; set;} = null;

    public Category Category {get; set;} = new Category();
}