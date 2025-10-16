using System.Data;

namespace TaskManagementProject.Data;

public interface IDbConnectionFactory
{
    IDbConnection CreateConnection();
}