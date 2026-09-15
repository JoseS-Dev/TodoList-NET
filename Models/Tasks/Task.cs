using TodoList_NET.Models.Users;
using TodoList_NET.Models.SubCategories;
using TodoList_NET.Models.Enums;

// Defino el modelo de las tareas del usuario
namespace TodoList_NET.Models.Tasks;

public class TaskUser
{
    public int Id {get; set;}
    public int UserId {get; set;}
    public int SubCategoryId {get; set;}
    public string TitleTask {get; set;} = string.Empty;
    public string? DescriptionTask {get; set;} = string.Empty;
    public DateTime? DueDate {get; set;} = null;
    public DateTime? CompletedDate {get; set;} = null;
    public StatuTask Status {get; set;} = StatuTask.Pendiente;
    public string? ReasonCancel {get; set;} = string.Empty;
    public DateTime CreatedAt {get; set;} = DateTime.Now;
    public DateTime UpdatedAt {get; set;} = DateTime.Now;
    public DateTime? DeletedAt {get; set;} = null;
    public User User {get; set;} = new User();
    public SubCategory SubCategory {get; set;} = new SubCategory();


}