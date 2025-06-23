using CodingTracker.Common.DataInterfaces;
using CodingTracker.Common.LoggingInterfaces;
using Microsoft.Extensions.Configuration;
using System.Data;
using System.Diagnostics;

namespace CodingTracker.Data.Configuration
{
    public class StartConfiguration : IStartConfiguration
    {
        private readonly IApplicationLogger _appLogger;
        private readonly IConfiguration _configuration;

        public string ConnectionString { get; private set; }

        public StartConfiguration(IApplicationLogger appLogger, IConfiguration configuration)
        {
            _appLogger = appLogger;
            _configuration = configuration;
            LoadConfiguration();
        }

        public void LoadConfiguration()
        {

            try
            {
                ConnectionString = _configuration.GetSection("DatabaseConfig:RemoteConnectionString").Value;
                TestDatabaseConnection();

                if (string.IsNullOrEmpty(ConnectionString))
                {
                    _appLogger.Error($"Connection string configuration is missing.");
                    throw new InvalidOperationException("Connection string configuration is missing.");
                }

                _appLogger.Info($"Configuration loaded successfully.");
            }
            catch (Exception ex)
            {
                _appLogger.Error($"Error loading configuration: {ex.Message}", ex);
                throw new InvalidOperationException("Error loading configuration: " + ex.Message, ex);
            }
        }

        public void TestDatabaseConnection()
        {
            using var connection = new Npgsql.NpgsqlConnection(ConnectionString);
            connection.Open();

            if (connection.State != System.Data.ConnectionState.Open)
            {
                throw new InvalidOperationException($"Failed to connect to database.");
            }
            _appLogger.Info($"Succesfully connected to database.");
        }


        public void LogCodingSessionsTableColumns()
        {
            try
            {
                using var connection = new Npgsql.NpgsqlConnection(ConnectionString);
                connection.Open();

                using var command = new Npgsql.NpgsqlCommand(@"
            SELECT column_name, data_type, is_nullable, column_default
            FROM information_schema.columns 
            WHERE table_name = 'CodingSessions' 
            AND table_schema = 'public'
            ORDER BY ordinal_position", connection);

                using var reader = command.ExecuteReader();

                _appLogger.Info($"\n=== Columns for CodingSessions table ===");
                while (reader.Read())
                {
                    string columnName = reader.GetString("column_name");
                    string dataType = reader.GetString("data_type");
                    string isNullable = reader.GetString("is_nullable");
                    string defaultValue = reader.IsDBNull("column_default") ? "NULL" : reader.GetString("column_default");

                    _appLogger.Info($"  {columnName} ({dataType}) - Nullable: {isNullable} - Default: {defaultValue}");
                }
            }
            catch (Exception ex)
            {
                _appLogger.Error($"Failed to retrieve columns for CodingSessions table: {ex.Message}", ex);
            }
        }
    }
}
