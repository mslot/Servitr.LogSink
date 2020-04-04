using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Servitr.LogSink.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Servitr.LogSink.TestConsole
{
    public class ConsoleService : IHostedService
    {
        private readonly ILogger<ConsoleService> _logger;
        private readonly ILogSink _logSink;

        public ConsoleService(ILogger<ConsoleService> logger,
            ILogSink logSink)
        {
            _logger = logger;
            _logSink = logSink;
        }
        public async Task StartAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("Start");
            _logSink.LogInformation("Log message {param1} {param2}", 0, "this is parameter 1", "this is parameter 2");
            await Task.CompletedTask;
        }

        public async Task StopAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("Stop");
            await Task.CompletedTask;
        }
    }
}
