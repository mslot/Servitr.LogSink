using System;

namespace Servitr.LogSink.Interfaces
{
    public interface ILogSink
    {
        void LogInformation<T>(string className, string methodName, int area);
        void LogError<T>(string className, string methodName, int area, Exception exception);
    }
}
