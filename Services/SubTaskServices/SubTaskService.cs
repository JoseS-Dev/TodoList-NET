using TodoList_NET.Data;
using TodoList_NET.Dtos;
using TodoList_NET.Models.Enums;
using TodoList_NET.Models.SubTasks;
using TodoList_NET.Services.SubTaskServices;

// Defino la clase del servicio de las subtareas
public class SubTaskService : ISubTaskService
{
    private readonly ApplicationDbContext _context;

    public SubTaskService(ApplicationDbContext context)
    {
        _context = context;
    }

    // Método para obtener todas las subtareas
    public async Task<List<SubTask>> GetAllSubTasksAsync(DtoParam param)
    {
        var query = _context.SubTasks.AsQueryable();

        // Si se proporciona un parámetro de búsqueda, se filtran las subtareas por título o descripción
        if(!string.IsNullOrEmpty(param.Search))
        {
            query = query.Where(st => st.TitleSubTask.Contains(param.Search) || st.DescriptionSubTask!.Contains(param.Search));
        }
        // Se aplica la paginación
        var subTasks = await query
        .Where(st => st.DeletedAt == null)
        .Skip((param.Page - 1) * param.Limit)
        .Take(param.Limit)
        .ToListAsync();

        return subTasks;
    }

    // Método para obtener todas las subtareas de una tarea
    public async Task<List<SubTask>> GetSubTasksByTaskIdAsync(int taskId, DtoParam param)
    {
        // Se verifica que exista la tarea en cuestión
        var existingTask = await _context.Tasks.FindAsync(taskId);
        if (existingTask == null)
        {
            throw new Exception("La tarea no existe");
        }
        // Si existe, se obtienen las subtareas de la tarea
        var query = _context.SubTasks.AsQueryable();
        if(!string.IsNullOrEmpty(param.Search))
        {
            query = query.Where(st => st.TitleSubTask.Contains(param.Search) || st.DescriptionSubTask!.Contains(param.Search));
        }
        var subTasks = await query
        .Where(st => st.TaskId == taskId && st.DeletedAt == null)
        .Skip((param.Page - 1) * param.Limit)
        .Take(param.Limit)
        .ToListAsync();

        return subTasks;
    }

    // Método para obtener una subtarea por su id
    public async Task<SubTask?> GetSubTaskByIdAsync(int id)
    {
        return await _context.SubTasks
        .Where(st => st.Id == id && st.DeletedAt == null)
        .FirstOrDefaultAsync();
    }

    // Método para crear una subtarea
    public async Task<DtoSubTaskResponse> CreateSubTaskAsync(DtoSubTaskCreate subTask)
    {
        // Se verifica que exista la tarea en cuestión
        bool taskExists = await _context.Tasks.AnyAsync(t => t.Id == subTask.TaskId && t.DeletedAt == null);
        if(!taskExists)
        {
            throw new Exception("La tarea no existe");
        }
        var newSubTask = new SubTask
        {
            TaskId = subTask.TaskId,
            TitleSubTask = subTask.TitleSubTask,
            DescriptionSubTask = subTask.DescriptionSubTask,
            DueDate = subTask.DueDate
        };
        _context.SubTasks.Add(newSubTask);
        await _context.SaveChangesAsync();
        return new DtoSubTaskResponse
        {
            Id = newSubTask.Id,
            TaskId = newSubTask.TaskId,
            TitleSubTask = newSubTask.TitleSubTask,
            DescriptionSubTask = newSubTask.DescriptionSubTask,
            DueDate = newSubTask.DueDate,
        };
    }

    // Método para actualizar una subtarea
    public async Task<DtoSubTaskResponse?> UpdateSubTaskAsync(int id, DtoSubTaskUpdate subTask)
    {
        var existingSubTask = await _context.SubTasks.FindAsync(id);
        if (existingSubTask == null || existingSubTask.DeletedAt != null)
        {
            return null;
        }
        existingSubTask.TitleSubTask = subTask.TitleSubTask ?? existingSubTask.TitleSubTask;
        existingSubTask.DescriptionSubTask = subTask.DescriptionSubTask ?? existingSubTask.DescriptionSubTask;
        existingSubTask.DueDate = subTask.DueDate ?? existingSubTask.DueDate;
        existingSubTask.UpdatedAt = DateTime.UtcNow;

        await _context.SaveChangesAsync();
        return new DtoSubTaskResponse
        {
            Id = existingSubTask.Id,
            TaskId = existingSubTask.TaskId,
            TitleSubTask = existingSubTask.TitleSubTask,
            DescriptionSubTask = existingSubTask.DescriptionSubTask,
            DueDate = existingSubTask.DueDate,
            CompletedDate = existingSubTask.CompletedDate,
            Status = existingSubTask.Status,
            ReasonCancel = existingSubTask.ReasonCancel
        };
    }

    // Método para actualizar el estado de una subtarea
    public async Task<DtoSubTaskResponse?> UpdateSubTaskStatusAsync(int id, DtoSubTaskUpdateStatus dtoSubTaskUpdateStatus)
    {
        var existingSubTask = await _context.SubTasks.FindAsync(id);
        if (existingSubTask == null || existingSubTask.DeletedAt != null)
        {
            return null;
        }
        // Si el estado a actualizar es "Completada", se actauliza la fecha de completado con la fecha actual
        if(dtoSubTaskUpdateStatus.Status == StatuTask.Completada)
        {
            existingSubTask.CompletedDate = DateTime.UtcNow;
        }
        else if(dtoSubTaskUpdateStatus.Status == StatuTask.Cancelada)
        {
            existingSubTask.ReasonCancel = dtoSubTaskUpdateStatus.ReasonCancel;
        }
        existingSubTask.Status = dtoSubTaskUpdateStatus.Status;
        existingSubTask.UpdatedAt = DateTime.UtcNow;

        await _context.SaveChangesAsync();
        return new DtoSubTaskResponse
        {
            Id = existingSubTask.Id,
            TaskId = existingSubTask.TaskId,
            TitleSubTask = existingSubTask.TitleSubTask,
            DescriptionSubTask = existingSubTask.DescriptionSubTask,
            DueDate = existingSubTask.DueDate,
            CompletedDate = existingSubTask.CompletedDate,
            Status = existingSubTask.Status,
            ReasonCancel = existingSubTask.ReasonCancel
        };
    }

    // Método para eliminar una subtarea
    public async Task<bool> DeleteSubTaskAsync(int id)
    {
        var existingSubTask = await _context.SubTasks.FindAsync(id);
        if (existingSubTask == null || existingSubTask.DeletedAt != null)
        {
            return false;
        }
        existingSubTask.DeletedAt = DateTime.UtcNow;
        await _context.SaveChangesAsync();
        return true;
    }
}