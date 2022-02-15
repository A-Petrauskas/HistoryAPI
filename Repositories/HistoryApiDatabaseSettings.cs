namespace Repositories
{
    public class HistoryApiDatabaseSettings : IHistoryApiDatabaseSettings
    {
        public string ConnectionString { get; set; }
        public string DatabaseName { get; set; }
    }
}
