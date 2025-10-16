using System.Collections.Generic;
using System.Threading.Tasks;
using Dapper;
using TaskManagementProject.Models;

namespace TaskManagementProject.Data;

public class TaskRepository : ITaskRepository
{
    private readonly IDbConnectionFactory _connectionFactory;

    public TaskRepository(IDbConnectionFactory connectionFactory)
    {
        _connectionFactory = connectionFactory;
    }

    public async Task<IEnumerable<TaskItem>> GetAllAsync()
    {
        using var connection = _connectionFactory.CreateConnection();
        return await connection.QueryAsync<TaskItem>(
            "SELECT * FROM Tasks ORDER BY Id");
    }

    public async Task<TaskItem?> GetByIdAsync(int id)
    {
        using var connection = _connectionFactory.CreateConnection();
        return await connection.QueryFirstOrDefaultAsync<TaskItem>(
            "SELECT * FROM Tasks WHERE Id = @id", new { id });
    }

    public async Task<int> AddAsync(TaskItem task)
    {
        using var connection = _connectionFactory.CreateConnection();
        var sql = @"
            INSERT INTO Tasks (Title, Description, IsCompleted, CreatedAt)
            VALUES (@Title, @Description, @IsCompleted, @CreatedAt);
            SELECT last_insert_rowid();
        ";
        return await connection.ExecuteScalarAsync<int>(sql, task);
    }

    public async Task<bool> UpdateStatusAsync(int id, bool isCompleted)
    {
        using var connection = _connectionFactory.CreateConnection();
        var rows = await connection.ExecuteAsync(
            "UPDATE Tasks SET IsCompleted = @isCompleted WHERE Id = @id",
            new { id, isCompleted });
        return rows > 0;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        using var connection = _connectionFactory.CreateConnection();
        var rows = await connection.ExecuteAsync(
            "DELETE FROM Tasks WHERE Id = @id", new { id });
        return rows > 0;
    }
}