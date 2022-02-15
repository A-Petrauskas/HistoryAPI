namespace Repositories
{
    public interface IHistoryApiDatabaseSettings
    {
        public string ConnectionString { get; set; }
        public string DatabaseName { get; set; }
    }
}
