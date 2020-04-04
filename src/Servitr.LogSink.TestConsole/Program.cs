using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Moq;
using Servitr.LogSink.EventIdMappers.InMemoryEventIdMapper;
using Servitr.LogSink.Interfaces;
using System;
using System.Threading.Tasks;

namespace Servitr.LogSink.TestConsole
{
    class Program
    {
        protected static SimpleLogSink _sink = null;
        static async Task Main(string[] args)
        {
            await CreateHostBuilder(args)
                                        .Build().
                                        RunAsync();
            //    var envMock = new Mock<IHostEnvironment>();
            //    envMock.Setup(env => env.EnvironmentName).Returns("TestConsole");

            //    //I want a real factory
            //    var serviceProvider = new ServiceCollection()
            //                                .AddLogging((config) =>
            //                                {
            //                                    config.AddConsole();
            //                                    config.AddDebug();
            //                                })

            //                                .BuildServiceProvider();

            //    var factory = serviceProvider.GetService<ILoggerFactory>();

            //    var eventIdMapper = new InMemoryEventIdMapper();

            //    _sink = new SimpleLogSink(
            //        envMock.Object,
            //        factory,
            //        eventIdMapper);
            //    //var l = factory.CreateLogger<Program>();
            //    //l.LogInformation("hej");
            //    _sink.LogInformation<Program>("Log message {param1}{param2}", 0, "this is parameter 1", "this is parameter 2");
            //}
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
        Host.CreateDefaultBuilder(args)
            .ConfigureServices((hostContext, services) =>
            {
                services.AddHostedService<ConsoleService>();
                services.AddSingleton<ILogSink, SimpleLogSink>();
                services.AddSingleton<IEventIdMapper, InMemoryEventIdMapper>();
            });

    }
}
