using System.Data;

namespace Contacts.Core.Data
{
    public interface IDbStrategy
    {
        IDbConnection GetConnection(string connectionString);
    }
}
