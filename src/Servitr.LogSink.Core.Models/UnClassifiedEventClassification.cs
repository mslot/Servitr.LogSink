using Servitr.LogSink.Interfaces;

namespace Servitr.LogSink.Core.Models
{
    public class UnClassifiedEventClassification : IEventClassification
    {
        public int EventId { get => Numbers.UNCLASSIFIED_EVENT_ID; set => _ = Numbers.UNCLASSIFIED_EVENT_ID; }
        public string Name { get; set; } = "UnClassifiedEvent";
        public string AreaName { get; set; }
    }
}
