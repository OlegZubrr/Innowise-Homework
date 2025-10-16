using System.Collections.Generic;
using System.Threading.Tasks;
using TaskManagementProject.Models;

namespace TaskManagementProject.Data;

public interface ITaskRepository
{
    Task<IEnumerable<TaskItem>> GetAllAsync();
    Task<TaskItem?> GetByIdAsync(int taskId);
    Task<int> AddAsync(TaskItem task);
    Task<bool> UpdateStatusAsync(int id,bool isCompleted);
    Task<bool> DeleteAsync(int id);
}