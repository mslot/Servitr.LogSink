using System;
using System.Collections.Generic;
using System.Text;

namespace Servitr.LogSink.Interfaces
{
    public interface IEventIdMapper
    {
        void AddClassification(string className, string methodName, int area, Exception exception, int eventId, string eventName);
        IEventClassification GetEventClassification(string className, string methodName, int area, Exception exception);
    }
}
