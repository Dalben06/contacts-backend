using System.Data;

namespace Contacts.Core.Data
{
    public class PostgreStrategy : IDbStrategy
    {
        public IDbConnection GetConnection(string connectionString)
        {
            return new Npgsql.NpgsqlConnection(connectionString);
        }
    }
}
