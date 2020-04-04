using System;
using System.Collections.Generic;
using System.Text;

namespace Servitr.LogSink.Interfaces
{
    public interface IEventIdMapper
    {
        void AddClassification(string className, string methodName, int areaId, string areaName, Type exceptionType, int eventId, string eventName);
        IEventClassification GetEventClassification(string className, string methodName, int areaId, Type exceptionType);
    }
}
