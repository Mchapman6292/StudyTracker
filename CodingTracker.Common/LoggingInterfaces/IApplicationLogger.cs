﻿using CodingTracker.Common.Entities.CodingSessionEntities;
using System.Diagnostics;

namespace CodingTracker.Common.LoggingInterfaces
{
    public interface IApplicationLogger
    {
        void LogActivity(string methodName, Action<Activity> logAction, Action<Activity> action);
        Task LogActivityAsync(string methodName, Func<Activity, Task> logAction, Func<Activity, Task> action);
        void LogUpdates(string methodName, params (string Name, object Value)[] updates);
        void LogCodingSessionEntity(CodingSessionEntity codingSessionEntity);
        void Info(string message);
        void Info(string message, params object[] propertyValues);
        void Debug(string message);
        void Debug(string message, params object[] propertyValues);
        void Warning(string message);
        void Warning(string message, params object[] propertyValues);
        void Error(string message, Exception ex);
        void Error(string message, Exception ex, params object[] propertyValues);
        void Error(string message, params object[] propertyValues);
        void Fatal(string message);
        void Fatal(string message, Exception ex);

    }
}
