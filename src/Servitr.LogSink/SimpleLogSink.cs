using Servitr.LogSink.Interfaces;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Runtime.CompilerServices;

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
        public void LogInformation<T>(string logMessage, int area, [CallerMemberName] string methodName="", params object[] logParameters)
        {
            var logger = CreateLogger<T>();
            string className = typeof(T).Name;
            var eventClassification = _eventIdMapper.GetEventClassification(className, methodName, area, null);

            logger.LogInformation(
                new EventId(eventClassification.EventId, eventClassification.Name),
                "[{environment}.{className}.{methodName}]: {message}",
                _env.EnvironmentName,
                className,
                methodName,
                logMessage,
                logParameters);
        }

        public void LogError<T>(int area, Exception exception, [CallerMemberName] string methodName = "")
        {
            var logger = CreateLogger<T>();
            string className = typeof(T).Name;
            var eventClassification = _eventIdMapper.GetEventClassification(className, methodName, area, exception);
            
            logger.LogError(
                new EventId(eventClassification.EventId, eventClassification.Name),
                exception,
                "[{className}.{methodName}]: threw exception in {environment}",
                className,
                methodName,
                _env.EnvironmentName);
        }

        //TODO: I think this can be rethought so a new isn't created on every call, but for now this is fine since the logic of this isn't pressured much 
        private ILogger<T> CreateLogger<T>()
        {
            return _loggerFactory.CreateLogger<T>();
        }
    }
}
