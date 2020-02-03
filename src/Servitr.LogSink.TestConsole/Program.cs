using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Moq;
using Servitr.LogSink.EventIdMappers.InMemoryEventIdMapper;
using System;

namespace Servitr.LogSink.TestConsole
{
    class Program
    {
        protected static SimpleLogSink _sink = null;
        static void Main(string[] args)
        {
            var envMock = new Mock<IHostEnvironment>();
            envMock.Setup(env => env.EnvironmentName).Returns("TestConsole");

            //I want a real factory
            var serviceProvider = new ServiceCollection()
                                        .AddLogging()
                                        .BuildServiceProvider();

            var factory = serviceProvider.GetService<ILoggerFactory>();

            var eventIdMapper = new InMemoryEventIdMapper();

            _sink = new SimpleLogSink(
                envMock.Object,
                factory,
                eventIdMapper);

            _sink.LogInformation<Program>("Log message {param1}{param2}", 0, "this is parameter 1", "this is parameter 2");
        }
    }
}
