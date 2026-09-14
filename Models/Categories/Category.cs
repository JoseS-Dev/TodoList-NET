using TodoList_NET.Models.Tasks;
using TodoList_NET.Models.SubCategories;
// Defino el modelo de las categorias de las tareas
namespace TodoList_NET.Models.Categories;

public class Category
{
    public int Id {get; set;}
    public string NameCategory {get; set;} = string.Empty;
    public string? DescriptionCategory {get; set;} = string.Empty;
    public DateTime CreatedAt {get; set;} = DateTime.Now;
    public DateTime UpdatedAt {get; set;} = DateTime.Now;
    public DateTime? DeletedAt {get; set;} = null;

    public ICollection<SubCategory> SubCategories { get; set; } = new List<SubCategory>();
    public ICollection<TaskUser> Tasks { get; set; } = new List<TaskUser>();
}