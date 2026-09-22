using TodoList_NET.Dtos;
using TodoList_NET.Models.Tasks;
using TodoList_NET.Services.TaskServices;

namespace TodoList_NET.Controllers.TaskController;

[ApiController]
[Route("api/[controller]")]
public class TasksController : ControllerBase
{
    private readonly ITaskService _taskService;

    public TasksController(ITaskService taskService)
    {
        _taskService = taskService;
    }

    [HttpGet]
    public async Task<ActionResult<List<TaskUser>>> GetAllTasks([FromQuery] DtoParam param)
    {
        var tasks = await _taskService.GetAllTasksAsync(param);
        return Ok(tasks);
    }

    [HttpGet("user/{userId}")]
    public async Task<ActionResult<List<TaskUser>>> GetTasksByUserId(int userId, [FromQuery] DtoParam param)
    {
        var tasks = await _taskService.GetTasksByUserIdAsync(userId, param);
        return Ok(tasks);
    }

    [HttpGet("subCategory/{subCategoryId}")]
    public async Task<ActionResult<List<TaskUser>>> GetTasksBySubCategoryId(int subCategoryId, [FromQuery] DtoParam param)
    {
        var tasks = await _taskService.GetTasksBySubCategoryIdAsync(subCategoryId, param);
        return Ok(tasks);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<TaskUser?>> GetTaskById(int id)
    {
        var task = await _taskService.GetTaskByIdAsync(id);
        if (task == null)
        {
            return NotFound();
        }
        return Ok(task);
    }

    [HttpPost]
    public async Task<ActionResult<DtoTaskResponse>> CreateTask(DtoTaskCreate task)
    {
        var createdTask = await _taskService.CreateTaskAsync(task);
        return CreatedAtAction(nameof(GetTaskById), new { id = createdTask.Id }, createdTask);
    }

    [HttpPatch("{id}")]
    public async Task<ActionResult<DtoTaskResponse?>> UpdateTask(int id, DtoTaskUpdate task)
    {
        var updatedTask = await _taskService.UpdateTaskAsync(id, task);
        if (updatedTask == null)
        {
            return NotFound();
        }
        return Ok(updatedTask);
    }

    [HttpPatch("status/{id}")]
    public async Task<ActionResult<DtoTaskResponse?>> UpdateTaskStatus(int id, DtoTaskUpdateStatus task)
    {
        var updatedTask = await _taskService.UpdateTaskStatusAsync(id, task);
        if (updatedTask == null)
        {
            return NotFound();
        }
        return Ok(updatedTask);
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteTask(int id)
    {
        var deleted = await _taskService.DeleteTaskAsync(id);
        if (!deleted)
        {
            return NotFound();
        }
        return NoContent();
    }
}