using Servitr.LogSink.Interfaces;
using System;

namespace Servitr.LogSink.Core.Models
{
    public class EventClassification : IEventClassification
    {
        public int EventId { get; set; }
        public string Name { get; set; }
        public string AreaName { get; set; }

    }
}
