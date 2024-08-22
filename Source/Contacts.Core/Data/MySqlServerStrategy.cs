using MySql.Data.MySqlClient;
using System.Data;

namespace Contacts.Core.Data
{
    public class MySqlServerStrategy : IDbStrategy
    {
        public IDbConnection GetConnection(string connectionString)
        {
            return new MySqlConnection(connectionString);
        }
    }
}
