using System;

namespace Servitr.LogSink.Interfaces
{
    public interface ILogSink
    {
        void LogInformation<T>(string logMessage, int area, string methodName, params object[] logParameters);
        void LogError<T>(int area, Exception exception, string methodName);
    }
}
