using TodoList_NET.Data;
using TodoList_NET.Dtos;
using TodoList_NET.Models.Enums;
using TodoList_NET.Models.Tasks;
using TodoList_NET.Services.TaskServices;

// Defino la clase del servicio de las tareas
public class TaskService : ITaskService
{
    private readonly ApplicationDbContext _context;

    public TaskService(ApplicationDbContext context)
    {
        _context = context;
    }

    // Método para obtener todas las tareas
    public async Task<List<TaskUser>> GetAllTasksAsync(DtoParam param)
    {
        var query = _context.Tasks.AsQueryable();

        // Si se proporciona un parámetro de búsqueda, se filtran las tareas por título o descripción
        if(!string.IsNullOrEmpty(param.Search))
        {
            query = query.Where(t => t.TitleTask.Contains(param.Search) || t.DescriptionTask!.Contains(param.Search));
        }
        // Se aplica la paginación
        var tasks = await query
        .Where(t => t.DeletedAt == null)
        .Skip((param.Page - 1) * param.Limit)
        .Take(param.Limit)
        .ToListAsync();

        return tasks;
    }

    // Método para obtener todas las tareas de un usuario
    public async Task<List<TaskUser>> GetTasksByUserIdAsync(int userId, DtoParam param)
    {
        // Se verifica que exista el usuario en cuestión
        var existingUser = await _context.Users.FindAsync(userId);
        if (existingUser == null)
        {
            throw new Exception("El usuario no existe");
        }
        // Si existe, se obtienen las tareas del usuario
        var query = _context.Tasks.AsQueryable();
        if(!string.IsNullOrEmpty(param.Search))
        {
            query = query.Where(t => t.TitleTask.Contains(param.Search) || t.DescriptionTask!.Contains(param.Search));
        }
        var tasks = await query
        .Where(t => t.UserId == userId && t.DeletedAt == null)
        .Skip((param.Page - 1) * param.Limit)
        .Take(param.Limit)
        .ToListAsync();

        return tasks;
    }

    // Método para obtener todas las tareas de una subcategoria
    public async Task<List<TaskUser>> GetTasksBySubCategoryIdAsync(int subCategoryId, DtoParam param)
    {
        // Se verifica que exista la subcategoria en cuestión
        var existingSubCategory = await _context.SubCategories.FindAsync(subCategoryId);
        if (existingSubCategory == null)
        {
            throw new Exception("La subcategoria no existe");
        }
        // Si existe, se obtienen las tareas de la subcategoria
        var query = _context.Tasks.AsQueryable();
        if(!string.IsNullOrEmpty(param.Search))
        {
            query = query.Where(t => t.TitleTask.Contains(param.Search) || t.DescriptionTask!.Contains(param.Search));
        }
        var tasks = await query
        .Where(t => t.SubCategoryId == subCategoryId && t.DeletedAt == null)
        .Skip((param.Page - 1) * param.Limit)
        .Take(param.Limit)
        .ToListAsync();

        return tasks;
    }

    // Método para obtener una tarea por su ID
    public async Task<TaskUser?> GetTaskByIdAsync(int id)
    {
        return await _context.Tasks
        .Where(t => t.Id == id && t.DeletedAt == null)
        .FirstOrDefaultAsync();
    }

    // Método para crear una nueva tarea
    public async Task<DtoTaskResponse> CreateTaskAsync(DtoTaskCreate task)
    {
        // Se valida que exista el usuario y la subcategoria en cuestión
        bool userExists = await _context.Users.AnyAsync(u => u.Id == task.UserId);
        bool subCategoryExists = await _context.SubCategories.AnyAsync(sc => sc.Id == task.SubCategoryId);
        if(!userExists || !subCategoryExists)
        {
            throw new Exception("El usuario o la subcategoria no existen");
        }
        // Si existen, se crea la tarea
        var newTask = new TaskUser
        {
            UserId = task.UserId,
            SubCategoryId = task.SubCategoryId,
            TitleTask = task.TitleTask,
            DescriptionTask = task.DescriptionTask,
            DueDate = task.DueDate
        };
        _context.Tasks.Add(newTask);
        await _context.SaveChangesAsync();
        return new DtoTaskResponse
        {
            Id = newTask.Id,
            UserId = newTask.UserId,
            SubCategoryId = newTask.SubCategoryId,
            TitleTask = newTask.TitleTask,
            DescriptionTask = newTask.DescriptionTask,
            DueDate = newTask.DueDate,
            Status = newTask.Status
        };
    }

    // Método para actualizar una tarea
    public async Task<DtoTaskResponse?> UpdateTaskAsync(int id, DtoTaskUpdate task)
    {
        var existingTask = await _context.Tasks.FindAsync(id);
        if (existingTask == null)
        {
            return null;
        }
        // Se valida que exista el usuario y la subcategoria en cuestión
        bool userExists = await _context.Users.AnyAsync(u => u.Id == task.UserId);
        bool subCategoryExists = await _context.SubCategories.AnyAsync(sc => sc.Id == task.SubCategoryId);
        if(!userExists || !subCategoryExists)
        {
            throw new Exception("El usuario o la subcategoria no existen");
        }
        // Se actualizan los campos de la tarea
        existingTask.UserId = task.UserId;
        existingTask.SubCategoryId = task.SubCategoryId;
        existingTask.TitleTask = task.TitleTask ?? existingTask.TitleTask;
        existingTask.DescriptionTask = task.DescriptionTask ?? existingTask.DescriptionTask;
        existingTask.DueDate = task.DueDate ?? existingTask.DueDate;
        existingTask.UpdatedAt = DateTime.UtcNow;
        await _context.SaveChangesAsync();
        return new DtoTaskResponse
        {
            Id = existingTask.Id,
            UserId = existingTask.UserId,
            SubCategoryId = existingTask.SubCategoryId,
            TitleTask = existingTask.TitleTask,
            DescriptionTask = existingTask.DescriptionTask,
            DueDate = existingTask.DueDate,
            Status = existingTask.Status
        };
    }

    // Método para actaulizar el estado de una tarea
    public async Task<DtoTaskResponse?> UpdateTaskStatusAsync(int id, DtoTaskUpdateStatus dtoTaskUpdateStatus)
    {
        // Se verifica que exista la tarea en cuestión
        var existingTask = await _context.Tasks.FindAsync(id);
        if (existingTask == null){
            return null;
        }
        // Si el estado a actualizar es "Completada", se actauliza la fecha de completado con la fecha actual
        if(dtoTaskUpdateStatus.Status == StatuTask.Completada)
        {
            existingTask.CompletedDate = DateTime.Now;
        }
        else if(dtoTaskUpdateStatus.Status == StatuTask.Cancelada)
        {
            existingTask.ReasonCancel = dtoTaskUpdateStatus.ReasonCancel;
        }
        existingTask.Status = dtoTaskUpdateStatus.Status;
        existingTask.UpdatedAt = DateTime.Now;
        await _context.SaveChangesAsync();
        return new DtoTaskResponse
        {
            Id = existingTask.Id,
            UserId = existingTask.UserId,
            SubCategoryId = existingTask.SubCategoryId,
            TitleTask = existingTask.TitleTask,
            DescriptionTask = existingTask.DescriptionTask,
            DueDate = existingTask.DueDate,
            CompletedDate = existingTask.CompletedDate,
            Status = existingTask.Status,
            ReasonCancel = existingTask.ReasonCancel
        };
    }

    public async Task<bool> DeleteTaskAsync(int id)
    {
        var existingTask = await _context.Tasks.FindAsync(id);
        if (existingTask == null)
        {
            return false;
        }
        existingTask.DeletedAt = DateTime.UtcNow;
        await _context.SaveChangesAsync();
        return true;
    } 
}