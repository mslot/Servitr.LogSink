using System;
using System.Collections.Generic;
using System.Text;

namespace Servitr.LogSink.Interfaces
{
    public interface IEventIdMapper
    {
        IEventClassification GetEventClassification(string className, string methodName, int area, Exception exception);
    }
}
