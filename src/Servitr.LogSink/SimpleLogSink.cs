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

        public void LogInformation<T>(string logMessage, object[] logParameters, int area = 0, Exception exception = null, [CallerMemberName]string callerName = "")
        {
            LogInternal<T>(LogType.Information, logMessage, logParameters, area, exception, callerName);
        }
        public void LogError<T>(string logMessage, object[] logParameters, int area = 0, Exception exception = null, [CallerMemberName] string callerName = "")
        {
            LogInternal<T>(LogType.Error, logMessage, logParameters, area, exception, callerName);
        }

        public void LogTrace<T>(string logMessage, object[] logParameters, int area = 0, Exception exception = null, [CallerMemberName] string callerName = "")
        {
            LogInternal<T>(LogType.Trace, logMessage, logParameters, area, exception, callerName);
        }

        public void LogCritical<T>(string logMessage, object[] logParameters, int area = 0, Exception exception = null, [CallerMemberName] string callerName = "")
        {
            LogInternal<T>(LogType.Critical, logMessage, logParameters, area, exception, callerName);
        }

        public void LogDebug<T>(string logMessage, object[] logParameters, int area = 0, Exception exception = null, [CallerMemberName] string callerName = "")
        {
            LogInternal<T>(LogType.Debug, logMessage, logParameters, area, exception, callerName);
        }

        public void LogWarning<T>(string logMessage, object[] logParameters, int area = 0, Exception exception = null, [CallerMemberName] string callerName = "")
        {
            LogInternal<T>(LogType.Warning, logMessage, logParameters, area, exception, callerName);
        }

        private void LogInternal<T>(LogType logType, string logMessage, object[] logParameters, int area, Exception exception, string callerName)
        {
            var logger = CreateLogger<T>();
            string className = typeof(T).Name;
            var eventClassification = _eventIdMapper.GetEventClassification(className, callerName, area, exception?.GetType());
            var eventId = new EventId(eventClassification.EventId, eventClassification.Name);

            switch (logType)
            {
                case LogType.Information:
                    logger.LogInformation(
                        eventId,
                        exception,
                        logMessage,
                        logParameters);
                    break;
                case LogType.Error:
                    logger.LogError(
                        eventId,
                        exception,
                        logMessage,
                        logParameters);
                    break;
                case LogType.Critical:
                    logger.LogCritical(
                        eventId,
                        exception,
                        logMessage,
                        logParameters);
                    break;
                case LogType.Debug:
                    logger.LogDebug(
                        eventId,
                        exception,
                        logMessage,
                        logParameters);
                    break;
                case LogType.Trace:
                    logger.LogTrace(
                        eventId,
                        exception,
                        logMessage,
                        logParameters);
                    break;
                case LogType.Warning:
                    logger.LogWarning(
                        eventId,
                        exception,
                        logMessage,
                        logParameters);
                    break;
            }
        }

        //TODO: I think this can be rethought so a new isn't created on every call, but for now this is fine since the logic of this isn't pressured much 
        private ILogger<T> CreateLogger<T>()
        {
            return _loggerFactory.CreateLogger<T>();
        }
    }
}
