using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Servitr.LogSink.Interfaces;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Servitr.LogSink.TestConsole
{
    public class ConsoleService : IHostedService
    {
        private readonly ILogger<ConsoleService> _logger;
        private readonly ILogSink _logSink;

        public ConsoleService(ILogger<ConsoleService> logger,
            ILogSink logSink,
            IEventIdMapper mapper)
        {
            _logger = logger;
            _logSink = logSink;

            mapper.AddClassification(nameof(ConsoleService), "StartAsync", 60, "Test", null, 6001, "something_happened");
            mapper.AddClassification(nameof(ConsoleService), "StartAsync", 60, "Test", typeof(Exception), 6099, "exception_happened");
        }
        public async Task StartAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("Start");
            _logger.LogInformation(new EventId(6001, "name"), "message {param1} {param2}", "this is parameter 1", "this is parameter 2");
            _logSink.LogInformation<ConsoleService>("Log message {param1} {param2}", new string[] { "this is parameter 1", "this is parameter 2" }, 60);

            try
            {
                throw new Exception();
            }
            catch (Exception e)
            {
                _logSink.LogInformation<ConsoleService>("Log message {param1} {param2}", new string[] { "this is parameter 1", "this is parameter 2" }, 60, e);
                _logSink.LogError<ConsoleService>("Log message {param1} {param2}", new string[] { "this is parameter 1", "this is parameter 2" }, 60, e);

            }

            await Task.CompletedTask;
        }

        public async Task StopAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("Stop");
            await Task.CompletedTask;
        }
    }
}
