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

        public void AddClassification(string className, string methodName, int areaId, string areaName, Type exceptionType, int eventId, string eventName)
        {
            string key = CalculateKey(className, methodName, areaId, exceptionType);
            if (!_mapper.ContainsKey(key))
            {
                var classification = CreateClassification(className, methodName, areaId, areaName, exceptionType, eventId, eventName);
                _mapper.Add(key, classification);
            }
        }

        public InMemoryEventIdMapper(Dictionary<string, IEventClassification> mapper) : base()
        {
            _mapper = mapper;
        }

        public IEventClassification GetEventClassification(string className, string methodName, int area, Type exceptionType)
        {
            IEventClassification classification = LookupEventClassification(className, methodName, area, exceptionType);

            return classification;
        }

        private IEventClassification CreateClassification(string className, string methodName, int areaId, string areaName, Type exceptionType, int eventId, string eventName)
        {
            string exceptionName = String.Empty;

            if(exceptionType != null)
            {
                exceptionName = nameof(exceptionType);
            }

            return new EventClassification
            {
                ExceptionName = exceptionName,
                ClassName = className,
                MethodName = methodName,
                AreaId = areaId,
                EventId = eventId,
                Name = eventName,
                AreaName = areaName
            };
        }

        private string CalculateKey(string className, string methodName, int area, Type exceptionType)
        {
            //TODO: maybe this can be more efficient using StringBuilder?
            string fourthLevelKey = string.Empty;
            string firstLevelKey = className;
            string secondLevelKey = $"{firstLevelKey}{methodName}";
            string thirdLevelKey = $"{secondLevelKey}{area}";

            if (exceptionType != null)
            {
                fourthLevelKey = $"{thirdLevelKey}{exceptionType.Name}";
            }
            else
            {
                fourthLevelKey = thirdLevelKey;
            }

            return fourthLevelKey;
        }

        private IEventClassification LookupEventClassification(
            string className,
            string methodName,
            int area,
            Type exceptionType)
        {
            string key = CalculateKey(className, methodName, area, exceptionType);
            if(!_mapper.TryGetValue(key, out IEventClassification resolvedClassification))
            {
                resolvedClassification = new UnClassifiedEventClassification();
            }

            if (exceptionType != null &&
                resolvedClassification is UnClassifiedEventClassification)
                resolvedClassification = new UnclassifiedExceptionEventClassification(exceptionType);

            return resolvedClassification;
        }
    }
}