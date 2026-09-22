using TodoList_NET.Models.Categories;
using TodoList_NET.Models.Tasks;

// Defino el modelo de las subcategorias
namespace TodoList_NET.Models.SubCategories;

public class SubCategory
{
    public int Id {get; set;}
    public int CategoryId {get; set;}
    public string NameSubCategory {get; set;} = string.Empty;
    public string? DescriptionSubCategory {get; set;} = string.Empty;
    public DateTime CreatedAt {get; set;} = DateTime.UtcNow;
    public DateTime UpdatedAt {get; set;} = DateTime.UtcNow;
    public DateTime? DeletedAt {get; set;} = null;

    public Category Category {get; set;} = new Category();
    public ICollection<TaskUser> Tasks { get; set; } = new List<TaskUser>();
}