namespace Contacts.Core.Configuration
{
    public sealed class Settings
    {
        public DatabaseContext DatabaseContext { get; set; }
        public Configuration Configuration { get; set; }
    }

    public sealed class Configuration
    {
        public string Secret { get; set; }
    }

    public sealed class DatabaseContext
    {
        public DatabaseType DatabaseType { get; set; }
        public string ConnectionString { get; set; }
    }

    public enum DatabaseType
    {
        MySql = 1,
        SqlServer = 2,
        Postgre = 3,
    }
}
