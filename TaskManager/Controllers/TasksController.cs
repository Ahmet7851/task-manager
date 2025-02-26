using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TaskManager.DTOs;
using TaskManager.Services;

namespace TaskManager.Controllers;

[Authorize]
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
    public async Task<ActionResult<IEnumerable<TaskDto>>> GetTasks([FromQuery] string? period)
    {
        var userId = GetUserId();
        var tasks = await _taskService.GetTasksAsync(userId, period ?? "all");
        return Ok(tasks);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<TaskDto>> GetTask(int id)
    {
        try
        {
            var userId = GetUserId();
            var task = await _taskService.GetTaskByIdAsync(userId, id);
            return Ok(task);
        }
        catch (InvalidOperationException ex)
        {
            return NotFound(new { message = ex.Message });
        }
    }

    [HttpPost]
    public async Task<ActionResult<TaskDto>> CreateTask(CreateTaskDto createTaskDto)
    {
        var userId = GetUserId();
        var task = await _taskService.CreateTaskAsync(userId, createTaskDto);
        return CreatedAtAction(nameof(GetTask), new { id = task.Id }, task);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<TaskDto>> UpdateTask(int id, UpdateTaskDto updateTaskDto)
    {
        try
        {
            var userId = GetUserId();
            var task = await _taskService.UpdateTaskAsync(userId, id, updateTaskDto);
            return Ok(task);
        }
        catch (InvalidOperationException ex)
        {
            return NotFound(new { message = ex.Message });
        }
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteTask(int id)
    {
        try
        {
            var userId = GetUserId();
            await _taskService.DeleteTaskAsync(userId, id);
            return NoContent();
        }
        catch (InvalidOperationException ex)
        {
            return NotFound(new { message = ex.Message });
        }
    }

    private int GetUserId()
    {
        var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        if (userIdClaim == null || !int.TryParse(userIdClaim, out var userId))
        {
            throw new InvalidOperationException("Kullanıcı kimliği bulunamadı.");
        }
        return userId;
    }
} 