using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Servitr.LogSink.EventIdMappers.InMemoryEventIdMapper;
using Servitr.LogSink.Interfaces;
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
