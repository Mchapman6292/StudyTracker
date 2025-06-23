using CodingTracker.Common.Entities.CodingSessionEntities;
using CodingTracker.Common.LoggingInterfaces;
using Serilog;
using System.Diagnostics;

namespace CodingTracker.Logging.ApplicationLoggers
{
    public class ApplicationLogger : IApplicationLogger
    {
        private readonly Serilog.ILogger _logger;

        public ApplicationLogger()
        {
            string desktopPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            string logPath = Path.Combine(desktopPath, "CodingTrackerLogs", "myapp.txt");

            Directory.CreateDirectory(Path.GetDirectoryName(logPath));

            _logger = new LoggerConfiguration()
                .MinimumLevel.Debug()
                .Enrich.FromLogContext()
                .WriteTo.Console()
                .WriteTo.File(logPath, rollingInterval: RollingInterval.Day)
                .CreateLogger();
        }

        public void LogActivity(string methodName, Action<Activity> logAction, Action<Activity> action)
        {
            using (var activity = new Activity(methodName).Start())
            {
                try
                {
                    logAction?.Invoke(activity);
                    action?.Invoke(activity);
                }
                catch (Exception ex)
                {
                    Error($"Exception in {methodName}. TraceID: {activity.TraceId}", ex);
                    throw;
                }
            }
        }



        public async Task LogActivityAsync(string methodName, Func<Activity, Task> logAction, Func<Activity, Task> action)
        {
            Activity activity = Activity.Current ?? new Activity(methodName).Start();

            try
            {
                if (logAction != null)
                    await logAction(activity);
                if (action != null)
                    await action(activity);
            }
            catch (Exception ex)
            {
                Error($"Exception in {methodName}. TraceID: {activity.TraceId}", ex);
                throw;
            }
            finally
            {
                if (Activity.Current == null)
                    activity.Stop();
            }
        }


        private void LogDatabaseError(Exception ex, string operationName, Stopwatch stopwatch)
        {
            stopwatch.Stop();
            Error($"Error executing {operationName}. Error: {ex.Message}. Execution Time: {stopwatch.ElapsedMilliseconds}ms. TraceID: {Activity.Current?.TraceId}");
        }

        public void LogUpdates(string methodName, params (string Name, object Value)[] updates)
        {
            using (var activity = new Activity(nameof(LogUpdates)).Start())
            {
                var updateEntries = updates
                    .Where(update => update.Value != null)
                    .Select(update => $"{update.Name}={update.Value}")
                    .ToList();

                if (updateEntries.Count > 0)
                {
                    string message = $"Updated {methodName}: {string.Join(", ", updateEntries)}. TraceID: {activity.TraceId}";
                    Info(message);
                }
            }
        }


        public void LogCodingSessionEntity(CodingSessionEntity codingSessionEntity)
        {
            string session =
                $"Values for codingSessionEntity \n" +
                $"-------SessionId : {codingSessionEntity.SessionId}.\n" +
                $"-------UserId : {codingSessionEntity.UserId}.\n" +
                $"-------StartDateLocal : {codingSessionEntity.StartDateUTC}.\n" +
                $"-------StartTimeLocal : {codingSessionEntity.StartTimeUTC}.\n" +
                $"-------EndDateLocal : {codingSessionEntity.EndDateUTC}.\n" +
                $"-------EndTimeLocal : {codingSessionEntity.EndTimeUTC}.\n" +
                $"-------DurationSeconds : {codingSessionEntity.DurationSeconds}.\n" +
                $"-------DurationHHMM : {codingSessionEntity.DurationHHMM}.\n" +
                $"-------GoalSet : {codingSessionEntity.GoalSet}.\n" +
                $"-------GoalSeconds : {codingSessionEntity.GoalSeconds}.\n" +
                $"-------GoalReached : {codingSessionEntity.GoalReached}.\n" +
                $"-------StudyProject : {codingSessionEntity.StudyProject}";

            Info(session);
        }

        public Task LogUpdatesAsync(string methodName, params (string Name, object Value)[] updates)
        {
            using (var activity = new Activity(nameof(LogUpdatesAsync)).Start())
            {
                var updateEntries = updates
                    .Where(update => update.Value != null)
                    .Select(update => $"{update.Name}={update.Value}")
                    .ToList();
                if (updateEntries.Count > 0)
                {
                    string message = $"Updated {methodName}: {string.Join(", ", updateEntries)}. TraceID: {activity.TraceId}";
                    Info(message);
                }
            }
            return Task.CompletedTask;
        }


        public void LogAnimatedTimerColumn()
        {

        }


    


        // Method overloading, allows multiple methods with same name but different parameter lists. 
        public void Info(string message) => _logger.Information(message); 
        public void Info(string message, params object[] propertyValues) => _logger.Information(message, propertyValues);

        public void Debug(string message) => _logger.Debug(message); 
        public void Debug(string message, params object[] propertyValues) => _logger.Debug(message, propertyValues);
        public void Warning(string message) => _logger.Warning(message); 
        public void Warning(string message, params object[] propertyValues) => _logger.Warning(message, propertyValues);
        public void Error(string message, Exception ex) => _logger.Error(ex, message); 
        public void Error(string message, Exception ex, params object[] propertyValues) => _logger.Error(ex, message, propertyValues);
        public void Error(string message, params object[] propertyValues) => _logger.Error(message, propertyValues);
        public void Fatal(string message) => _logger.Fatal(message);
        public void Fatal(string message, Exception ex) => _logger.Fatal(ex, message);
    }
}
