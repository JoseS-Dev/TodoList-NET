using TodoList_NET.Models.Enums;

namespace TodoList_NET.Dtos;

// Defino el dto de creación de una tarea
public class DtoTaskCreate
{
   public int UserId {get; set;}
   public int SubCategoryId {get; set;}
   public string TitleTask {get; set;} = string.Empty;
    public string? DescriptionTask {get; set;} = string.Empty;
    public DateTime? DueDate {get; set;} = null;

}

// Defino el dto de actualización de una tarea
public class DtoTaskUpdate
{
   public int UserId {get; set;}
   public int SubCategoryId {get; set;}
   public string? TitleTask {get; set;} = string.Empty;
    public string? DescriptionTask {get; set;} = string.Empty;
    public DateTime? DueDate {get; set;} = null;

}

// Defino el dto de la actualización del estado
public class DtoTaskUpdateStatus
{
    public StatuTask Status {get; set;} = StatuTask.Pendiente;
    public DateTime? CompletedDate {get; set;} = null;
    public string? ReasonCancel {get; set;} = string.Empty;
}

// Defino el dto de la respuesta de una tarea
public class DtoTaskResponse
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

}