using Microsoft.EntityFrameworkCore;
using TaskManager.Data;
using TaskManager.DTOs;
using TaskManager.Models;

namespace TaskManager.Services;

public class TaskService : ITaskService
{
    private readonly ApplicationDbContext _context;

    public TaskService(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<TaskDto>> GetTasksAsync(int userId, string period)
    {
        var query = _context.Tasks.Where(t => t.UserId == userId);

        var now = DateTime.Now.Date;
        query = period.ToLower() switch
        {
            "daily" => query.Where(t => t.StartDate.Date == now),
            "weekly" => query.Where(t => t.StartDate.Date >= now.AddDays(-7) && t.StartDate.Date <= now),
            "monthly" => query.Where(t => t.StartDate.Date >= now.AddMonths(-1) && t.StartDate.Date <= now),
            _ => query
        };

        var tasks = await query.OrderBy(t => t.StartDate).ToListAsync();
        return tasks.Select(MapToDto);
    }

    public async Task<TaskDto> GetTaskByIdAsync(int userId, int taskId)
    {
        var task = await GetUserTaskAsync(userId, taskId);
        return MapToDto(task);
    }

    public async Task<TaskDto> CreateTaskAsync(int userId, CreateTaskDto createTaskDto)
    {
        var task = new UserTask
        {
            UserId = userId,
            Title = createTaskDto.Title,
            Description = createTaskDto.Description,
            StartDate = createTaskDto.StartDate,
            EndDate = createTaskDto.EndDate,
            Status = UserTaskStatus.Todo
        };

        _context.Tasks.Add(task);
        await _context.SaveChangesAsync();

        return MapToDto(task);
    }

    public async Task<TaskDto> UpdateTaskAsync(int userId, int taskId, UpdateTaskDto updateTaskDto)
    {
        var task = await GetUserTaskAsync(userId, taskId);

        task.Title = updateTaskDto.Title;
        task.Description = updateTaskDto.Description;
        task.StartDate = updateTaskDto.StartDate;
        task.EndDate = updateTaskDto.EndDate;
        task.Status = updateTaskDto.Status;

        await _context.SaveChangesAsync();

        return MapToDto(task);
    }

    public async Task DeleteTaskAsync(int userId, int taskId)
    {
        var task = await GetUserTaskAsync(userId, taskId);
        _context.Tasks.Remove(task);
        await _context.SaveChangesAsync();
    }

    private async Task<UserTask> GetUserTaskAsync(int userId, int taskId)
    {
        var task = await _context.Tasks.FirstOrDefaultAsync(t => t.Id == taskId && t.UserId == userId);
        if (task == null)
        {
            throw new InvalidOperationException("Görev bulunamadı.");
        }
        return task;
    }

    private static TaskDto MapToDto(UserTask task)
    {
        return new TaskDto
        {
            Id = task.Id,
            Title = task.Title,
            Description = task.Description,
            StartDate = task.StartDate,
            EndDate = task.EndDate,
            Status = task.Status
        };
    }
} 