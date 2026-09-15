using TodoList_NET.Dtos;
using TodoList_NET.Models.SubTasks;

namespace TodoList_NET.Services.SubTaskServices;

// Defino la interfaz del servicio de las subtareas
public interface ISubTaskService
{
    Task<List<SubTask>> GetAllSubTasksAsync(DtoParam param);
    Task<List<SubTask>> GetSubTasksByTaskIdAsync(int taskId, DtoParam param);
    Task<SubTask?> GetSubTaskByIdAsync(int id);
    Task<DtoSubTaskResponse> CreateSubTaskAsync(DtoSubTaskCreate subTask);
    Task<DtoSubTaskResponse?> UpdateSubTaskAsync(int id, DtoSubTaskUpdate subTask);
    Task<DtoSubTaskResponse?> UpdateSubTaskStatusAsync(int id, DtoSubTaskUpdateStatus subTask);
    Task<bool> DeleteSubTaskAsync(int id);
}