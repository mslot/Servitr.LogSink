using System;
using System.Collections.Generic;
using Servitr.LogSink.Core.Models;
using Servitr.LogSink.Interfaces;

namespace Servitr.LogSink.EventIdMappers.InMemoryEventIdMapper
{
    public class InMemoryEventIdMapper : IEventIdMapper
    {
        private Dictionary<string, IEventClassification> _mapper;

        public InMemoryEventIdMapper()
        {
            if (_mapper == null)
            {
                _mapper = new Dictionary<string, IEventClassification>();
            }
        }

        public void AddClassification(string className, string methodName, int area, Exception exception, int eventId, string eventName)
        {
            string key = CalculateKey(className, methodName, area, exception);
            if (!_mapper.ContainsKey(key))
            {
                IEventClassification classification = CreateClassification(className, methodName, area, exception, eventId, eventName);
                _mapper.Add(key, classification);
            }
        }

        private IEventClassification CreateClassification(string className, string methodName, int area, Exception exception, int eventId, string eventName)
        {
            return new EventClassification
            {
                EventId = eventId,
                Name = eventName
            };
        }

        private string CalculateKey(string className, string methodName, int area, Exception exception)
        {
            //TODO: maybe this can be more efficient using StringBuilder? Test it!
            string fourthLevelKey = string.Empty;
            string firstLevelKey = className;
            string secondLevelKey = $"{firstLevelKey}{methodName}";
            string thirdLevelKey = $"{secondLevelKey}{area}";

            if (exception != null)
            {
                fourthLevelKey = $"{thirdLevelKey}{nameof(exception)}";
            }
            else
            {
                fourthLevelKey = thirdLevelKey;
            }

            return fourthLevelKey;
        }

        public InMemoryEventIdMapper(Dictionary<string, IEventClassification> mapper) : base()
        {
            _mapper = mapper;
        }

        public IEventClassification GetEventClassification(string className, string methodName, int area, Exception exception)
        {
            IEventClassification classification = LookupEventClassification(className, methodName, area, exception);

            return classification;
        }

        private IEventClassification LookupEventClassification(
            string className,
            string methodName,
            int area,
            Exception exception)
        {
            string key = CalculateKey(className, methodName, area, exception);
            if(!_mapper.TryGetValue(key, out IEventClassification resolvedClassification))
            {
                resolvedClassification = new UnClassifiedEventClassification();
            }

            if (exception != null &&
                resolvedClassification is UnClassifiedEventClassification)
                resolvedClassification = new UnclassifiedExceptionEventClassification(exception);

            return resolvedClassification;
        }
    }
}