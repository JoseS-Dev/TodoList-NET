using TodoList_NET.Models.Enums;
using TodoList_NET.Models.Tasks;
// Defino el modelo de las subcategorias de las categorias
namespace TodoList_NET.Models.SubTasks;

public class SubTask
{
    public int Id {get; set;}
    public int TaskId {get; set;}
    public string TitleSubTask {get; set;} = string.Empty;
    public string? DescriptionSubTask {get; set;} = string.Empty;
    public DateTime? DueDate {get; set;} = null;
    public DateTime? CompletedDate {get; set;} = null;
    public StatuTask Status {get; set;} = StatuTask.Pendiente;
    public string? ReasonCancel {get; set;} = string.Empty;
    public DateTime CreatedAt {get; set;} = DateTime.Now;
    public DateTime UpdatedAt {get; set;} = DateTime.Now;
    public DateTime? DeletedAt {get; set;} = null;

    public TaskUser TaskUser {get; set;} = new TaskUser();
}