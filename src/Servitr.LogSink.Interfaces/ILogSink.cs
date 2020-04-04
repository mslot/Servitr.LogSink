using System;
using System.Runtime.CompilerServices;

namespace Servitr.LogSink.Interfaces
{
    public interface ILogSink
    {
        void LogInformation<T>(string logMessage, object[] logParameters, int area=0, Exception exception=null, [CallerMemberName]string callerName="");
    }
}
