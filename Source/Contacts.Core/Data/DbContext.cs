using System.Data;

namespace Contacts.Core.Data
{
    public sealed class DbContext
    {
        private readonly DbFactory _dbFactory;
        public IDbConnection DbConnection { get; set; }
        public DbContext(DbFactory dbFactory)
        {
            _dbFactory = dbFactory;
            this.DbConnection = _dbFactory.GetConnection();
        }
    }
}
