// Defino la interfaz del servicio de las tareas
using TodoList_NET.Dtos;
using TodoList_NET.Models.Tasks;

namespace TodoList_NET.Services.TaskServices;
public interface ITaskService
{
    Task<List<TaskUser>> GetAllTasksAsync(DtoParam param);
    Task<List<TaskUser>> GetTasksByUserIdAsync(int userId, DtoParam param);
    Task<List<TaskUser>> GetTasksBySubCategoryIdAsync(int subCategoryId, DtoParam param);
    Task<TaskUser?> GetTaskByIdAsync(int id);
    Task<DtoTaskResponse> CreateTaskAsync(DtoTaskCreate task);
    Task<DtoTaskResponse?> UpdateTaskAsync(int id, DtoTaskUpdate task);
    Task<DtoTaskResponse?> UpdateTaskStatusAsync(int id, DtoTaskUpdateStatus task);
    Task<bool> DeleteTaskAsync(int id);
}