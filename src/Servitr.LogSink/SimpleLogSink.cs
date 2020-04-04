using Servitr.LogSink.Interfaces;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Runtime.CompilerServices;
using System.Collections.Generic;

namespace Servitr.LogSink
{
    public class SimpleLogSink : ILogSink
    {
        private readonly IHostEnvironment _env;
        private readonly ILoggerFactory _loggerFactory;
        private readonly IEventIdMapper _eventIdMapper;

        //TODO: Should we tear out the logger factory and make our own factory so other frameworks than Application Insights could be used?
        public SimpleLogSink(
            IHostEnvironment env,
            ILoggerFactory loggerFactory,
            IEventIdMapper eventIdMapper)
        {
            _env = env;
            _loggerFactory = loggerFactory;
            _eventIdMapper = eventIdMapper;
        }
        public void LogInformation<T>(string logMessage, int area, params object[] logParameters)
        {
            var logger = CreateLogger<T>();
            string className = typeof(T).Name;
            var eventClassification = _eventIdMapper.GetEventClassification(className, callerName, area, null);

            logger.LogInformation(
                new EventId(eventClassification.EventId, eventClassification.Name),
                logMessage,
                logParameters);
        }

        //TODO: I think this can be rethought so a new isn't created on every call, but for now this is fine since the logic of this isn't pressured much 
        private ILogger<T> CreateLogger<T>()
        {
            return _loggerFactory.CreateLogger<T>();
        }
    }
}
