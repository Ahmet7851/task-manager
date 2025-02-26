using TaskManager.DTOs;

namespace TaskManager.Services;

public interface ITaskService
{
    Task<IEnumerable<TaskDto>> GetTasksAsync(int userId, string period);
    Task<TaskDto> GetTaskByIdAsync(int userId, int taskId);
    Task<TaskDto> CreateTaskAsync(int userId, CreateTaskDto createTaskDto);
    Task<TaskDto> UpdateTaskAsync(int userId, int taskId, UpdateTaskDto updateTaskDto);
    Task DeleteTaskAsync(int userId, int taskId);
} 