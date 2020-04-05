using Servitr.LogSink.Interfaces;

namespace Servitr.LogSink.Core.Models
{
    public class EventClassification : IEventClassification
    {
        public int EventId { get; set; }
        public string Name { get; set; }
        public string AreaName { get; set; }
        public string ClassName { get; set; }
        public string MethodName { get; set; }
        public int AreaId { get; set; }
        public string ExceptionName { get; set; }
    }
}
