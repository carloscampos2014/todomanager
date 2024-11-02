using System.Data;

namespace TodoManager.Database.Sqlite.Factories;
public interface IConnectionFactory
{
    IDbConnection CreateConnection();
}
