using System;

namespace Servitr.LogSink.Interfaces
{
    public interface ILogSink
    {
        void LogInformation<T>(int area, string methodName);
        void LogError<T>(int area, Exception exception, string methodName);
    }
}
