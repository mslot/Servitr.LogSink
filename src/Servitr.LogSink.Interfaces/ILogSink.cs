using System;

namespace Servitr.LogSink.Interfaces
{
    public interface ILogSink
    {
        void LogInformation<T>(string logMessage, int area, params object[] logParameters);
    }
}
