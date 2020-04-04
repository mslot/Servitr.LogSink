using System;

namespace Servitr.LogSink.Interfaces
{
    public interface ILogSink
    {
        void LogInformation<T>(string logMessage,int area=0, params object[] logParameters);
    }
}
