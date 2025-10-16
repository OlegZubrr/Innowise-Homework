using System.Data;
using System.IO;
using Microsoft.Data.Sqlite;

namespace TaskManagementProject.Data;

public class DbConnectionFactory : IDbConnectionFactory
{
    private readonly string _connectionString;

    public DbConnectionFactory(string connectionString)
    {
        _connectionString = connectionString;
    }

    public IDbConnection CreateConnection()
    {
        return new SqliteConnection(_connectionString);
    }

    public void InitializeDatabase()
    {
        var builder = new SqliteConnectionStringBuilder(_connectionString);
        var dbPath = builder.DataSource;

        var dir = Path.GetDirectoryName(dbPath);
        if (!string.IsNullOrEmpty(dir))
            Directory.CreateDirectory(dir);

        using var connection = new SqliteConnection(_connectionString);
        connection.Open();

        using var command = connection.CreateCommand();
        command.CommandText = @"
        CREATE TABLE IF NOT EXISTS Tasks (
        Id INTEGER PRIMARY KEY AUTOINCREMENT,
        Title TEXT NOT NULL,
        Description TEXT,
        IsCompleted INTEGER NOT NULL DEFAULT 0,
        CreatedAt TEXT DEFAULT CURRENT_TIMESTAMP
    );
";

        command.ExecuteNonQuery();
    }
}