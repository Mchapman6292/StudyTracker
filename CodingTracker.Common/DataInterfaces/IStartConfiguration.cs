namespace CodingTracker.Common.DataInterfaces
{
    public interface IStartConfiguration
    {
        public string ConnectionString { get; }

        void LoadConfiguration();
        void TestDatabaseConnection();
        void LogCodingSessionsTableColumns();
    }
}