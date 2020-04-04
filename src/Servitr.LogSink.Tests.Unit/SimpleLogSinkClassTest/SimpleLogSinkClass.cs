using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Moq;
using Servitr.LogSink.EventIdMappers.InMemoryEventIdMapper;
using Servitr.LogSink.Tests.Unit.SimpleLogSinkClassTest;
using Xunit;

namespace Servitr.LogSink.Tests.Unit.SimpleLogSinkClass
{
    public class SimpleLogSinkClass
    {
        public class LogInformationMethod
        {
            protected SimpleLogSink _sink = null;

            public LogInformationMethod()
            {
                var envMock = new Mock<IHostEnvironment>();
                envMock.Setup(env => env.EnvironmentName).Returns("UnitTest");

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
            }

            [Fact]
            public void SimpleCall()
            {
                _sink.LogInformation<LogInformationMethod>("Log message {param1}{param2}", new string[] { "this is parameter 1", "this is parameter 2" }, EventTest.COMMON_AREA);
            }
        }

        public class LogErrorMethod
        {
            protected SimpleLogSink _sink = null;

            public LogErrorMethod()
            {
                var envMock = new Mock<IHostEnvironment>();
                envMock.Setup(env => env.EnvironmentName).Returns("UnitTest");

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
            }

            [Fact]
            public void SimpleCall()
            {
                //_sink.LogError<LogErrorMethod>(EventTest.COMMON_AREA, new System.Exception("test"));
            }
        }
    }
}
