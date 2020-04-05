# Servitr.LogSink
This project is intended as a _pure experiment_ and is put into the world so i can test two things:

1. How to create a yaml DevOps pipeline, GitHub pages and nuget repos for library development (and how to use GitVersion properly when branching)
2. To look more into a setup where I can log with event ids as a central part og an API, and what that potentially could lead to of gains

This will _never_ be done, and _shouldn't_ be used for production grade applications. _Not all `Log*` methods are supported_. Please also note that I will use this for experimenting, and I will break the API sometimes.

But if you like this, please clone it, or create some pull requests, that provide further enhancements to the API. It could be fun to follow through.

The most important take away with this project is the baseline setup for library development, that you can use or be inspired by.

# Nuget feeds
I have two nuget feeds:

1. A beta nuget feed:  [on Azure DevOps](https://dev.azure.com/servitr/Servitr.LogSink/_packaging?_a=feed&feed=Servitr.LogSink) - called "beta nuget feed". These packages can change a lot from version to version. Expect the API to break!
2. A stable nuget feed: [On github.com](https://github.com/mslot?tab=packages) - called "release nuget feed". These packages here, is stable packages, that has been tested enough in beta. These packages will not change that much from version to version. There can be prerelease packages here, that will act must like a release candidate.

I haven't yet set up a seperate account for my nuget packages, so they are going to be located on my private account on github for now.

# Application Insights
This project is using application insights, but other logging sources could be used. Please note that application insights has no reserved event id numbers, personally I start my count relative high when classifing events: 6000. 

# Example of use
A compact example is included in the repo, in the `TestConsole.ConsoleService`:

```csharp
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
            mapper.AddClassification(nameof(ConsoleService), "StartAsync", 60, "Test", typeof(OutOfMemoryException), 6099, "exception_happened");
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

```

## Install
Create a personal token for your [github account](https://github.com/settings/tokens) with the read:packages scope. SAVE THE TOKEN. Then add a NuGet.config to your project root (same level as your solution):

```xml
<?xml version="1.0" encoding="utf-8"?>
<configuration>
    <packageSources>
        <clear />
        <add key="nuget.org" value="https://api.nuget.org/v3/index.json"/>
        <add key="github" value="https://nuget.pkg.github.com/mslot/index.json" />
    </packageSources>
    <packageSourceCredentials>
        <github>
            <add key="Username" value="[YOUR_USERNAME]" />
            <add key="ClearTextPassword" value="[SAVED_TOKEN]" />
        </github>
    </packageSourceCredentials>
</configuration>

```

Remember that it is your _username_, _not your email_. After that cd to where your csproj file is that is going to have a reference to the `Servitr.LogSink` library and add the nuget `dotnet add [your_project_name].csproj package Servitr.LogSink --version [latest_version]`.

As of today this can't be done through the Visual Studio UI. Remember to not add the NuGet.config to git (it contains sensitive information). There is other ways to keep that out of the NuGet.config. Read more [here](https://docs.microsoft.com/en-us/nuget/reference/nuget-config-file#using-environment-variables). I think it is possible, after you have added the package once from the commandline, to update it from the Visual Studio UI.

## Call convention
The call convention might seem a bit more complicated and longer, but please have in mind that we have `EventId` in focus, so there is actually not that much difference:

```csharp
_logger.LogInformation(new EventId(6001, "name"), "message {param1} {param2}", "this is parameter 1", "this is parameter 2");
_logSink.LogInformation<ConsoleService>("Log message {param1} {param2}", new string[] { "this is parameter 1", "this is parameter 2" }, 60);
```

We are talking about 14 characters. But it still pains my eyes that we have to apply `ConsoleService` and new'ing the log argument list (but this is needed if we are going to get the caller name).