using Servitr.LogSink.Interfaces;

namespace Servitr.LogSink.Core.Models
{
    public class UnClassifiedEventClassification : IEventClassification
    {
        private string _name = "UnClassifiedEvent";
        public int EventId { get => 0; set => _ = 0; }
        public string Name { get => _name; set => _name=value; }
        public string AreaName { get; set; }
    }
}
