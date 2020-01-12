using System;
using System.Collections.Generic;
using System.Text;

namespace Servitr.LogSink.Interfaces
{
    public interface IEventClassification
    {
        int EventId { get; set; }
        string Name { get; set; }
    }
}
