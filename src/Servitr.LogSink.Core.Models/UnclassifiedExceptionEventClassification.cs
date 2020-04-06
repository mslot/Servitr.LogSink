using Servitr.LogSink.Interfaces;
using System;

namespace Servitr.LogSink.Core.Models
{
    public class UnclassifiedExceptionEventClassification : IEventClassification
    {
        public Type ExceptionType { get; private set; }
        public int EventId { get => Numbers.UNCLASSIFIED_EXCEPTION_EVENT_ID; set => _ = Numbers.UNCLASSIFIED_EXCEPTION_EVENT_ID; }
        public string Name { get; set; } = "UnClassifiedExceptionEvent";
        public string AreaName { get; set; }

        public UnclassifiedExceptionEventClassification(Type exceptionType)
        {
            ExceptionType = exceptionType;
        }
    }
}
