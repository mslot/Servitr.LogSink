using System;
using System.Runtime.CompilerServices;

namespace Servitr.LogSink.Interfaces
{
    public interface ILogSink
    {
        void LogInformation<T>(string logMessage, object[] logParameters, int area = 0, Exception exception = null, [CallerMemberName]string callerName = "");
        void LogError<T>(string logMessage, object[] logParameters, int area = 0, Exception exception = null, [CallerMemberName]string callerName = "");
        void LogTrace<T>(string logMessage, object[] logParameters, int area = 0, Exception exception = null, [CallerMemberName]string callerName = "");
        void LogCritical<T>(string logMessage, object[] logParameters, int area = 0, Exception exception = null, [CallerMemberName]string callerName = "");
        void LogDebug<T>(string logMessage, object[] logParameters, int area = 0, Exception exception = null, [CallerMemberName]string callerName = "");
        void LogWarning<T>(string logMessage, object[] logParameters, int area = 0, Exception exception = null, [CallerMemberName]string callerName = "");
    }
}
