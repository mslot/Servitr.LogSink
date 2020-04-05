using Servitr.LogSink.Interfaces;

namespace Servitr.LogSink.Core.Models
{
    public class UnClassifiedEventClassification : IEventClassification
    {
        public int EventId { get => 0; set => _ = 0; }
        public string Name { get; set; } = "UnClassifiedEvent";
        public string AreaName { get; set; }
    }
}
