using TodoList_NET.Dtos;
using TodoList_NET.Models.SubTasks;
using TodoList_NET.Services.SubTaskServices;

namespace TodoList_NET.Controllers.SubTasksController;

[ApiController]
[Route("api/[controller]")]
public class SubTasksController : ControllerBase
{
    private readonly ISubTaskService _subTaskService;

    public SubTasksController(ISubTaskService subTaskService)
    {
        _subTaskService = subTaskService;
    }

    [HttpGet]
    public async Task<ActionResult<List<SubTask>>> GetAllSubTasks([FromQuery] DtoParam param)
    {
        var subTasks = await _subTaskService.GetAllSubTasksAsync(param);
        return Ok(subTasks);
    }

    [HttpGet("task/{taskId}")]
    public async Task<ActionResult<List<SubTask>>> GetSubTasksByTaskId(int taskId, [FromQuery] DtoParam param)
    {
        var subTasks = await _subTaskService.GetSubTasksByTaskIdAsync(taskId, param);
        return Ok(subTasks);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<SubTask?>> GetSubTaskById(int id)
    {
        var subTask = await _subTaskService.GetSubTaskByIdAsync(id);
        if (subTask == null)
        {
            return NotFound();
        }
        return Ok(subTask);
    }

    [HttpPost]
    public async Task<ActionResult<DtoSubTaskResponse>> CreateSubTask(DtoSubTaskCreate subTask)
    {
        var createdSubTask = await _subTaskService.CreateSubTaskAsync(subTask);
        return CreatedAtAction(nameof(GetSubTaskById), new { id = createdSubTask.Id }, createdSubTask);
    }

    [HttpPatch("{id}")]
    public async Task<ActionResult<DtoSubTaskResponse?>> UpdateSubTask(int id, DtoSubTaskUpdate subTask)
    {
        var updatedSubTask = await _subTaskService.UpdateSubTaskAsync(id, subTask);
        if (updatedSubTask == null)
        {
            return NotFound();
        }
        return Ok(updatedSubTask);
    }

    [HttpPatch("status/{id}")]
    public async Task<ActionResult<DtoSubTaskResponse?>> UpdateSubTaskStatus(int id, DtoSubTaskUpdateStatus subTask)
    {
        var updatedSubTask = await _subTaskService.UpdateSubTaskStatusAsync(id, subTask);
        if (updatedSubTask == null)
        {
            return NotFound();
        }
        return Ok(updatedSubTask);
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteSubTask(int id)
    {
        var deleted = await _subTaskService.DeleteSubTaskAsync(id);
        if (!deleted)
        {
            return NotFound();
        }
        return NoContent();
    }
}
