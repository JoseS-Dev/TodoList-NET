using TodoList_NET.Models.Enums;

namespace TodoList_NET.Dtos;

// Defino el dto de creación para una subtarea de una tarea principal
public class DtoSubTaskCreate
{
    public int TaskId {get; set;}
    public string TitleSubTask {get; set;} = string.Empty;
    public string? DescriptionSubTask {get; set;} = string.Empty;
    public DateTime? DueDate {get; set;} = null;
}

// Defino el dto de actualización para una subtarea
public class DtoSubTaskUpdate
{
    public int TaskId {get; set;}
    public string? TitleSubTask {get; set;} = string.Empty;
    public string? DescriptionSubTask {get; set;} = string.Empty;
    public DateTime? DueDate {get; set;} = null;
}

// Defino el dto de actaulización del estado de una subtarea
public class DtoSubTaskUpdateStatus
{
    public StatuTask Status {get; set;} = StatuTask.Pendiente;
    public DateTime? CompletedDate {get; set;} = null;
    public string? ReasonCancel {get; set;} = string.Empty;
}

// Defino el dto de respuesta de una subtarea
public class DtoSubTaskResponse
{
    public int Id {get; set;}
    public int TaskId {get; set;}
    public string TitleSubTask {get; set;} = string.Empty;
    public string? DescriptionSubTask {get; set;} = string.Empty;
    public DateTime? DueDate {get; set;} = null;
    public DateTime? CompletedDate {get; set;} = null;
    public StatuTask Status {get; set;} = StatuTask.Pendiente;
    public string? ReasonCancel {get; set;} = string.Empty;

}