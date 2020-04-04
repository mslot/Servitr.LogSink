# Servitr.LogSink
This project is intended as a _pure experiment_ and is put into the world so i can test two things:

1. How to create a yaml DevOps pipeline, GitHub pages and nuget repos for library development (and how to use GitVersion properly when branching)
2. To look more into a setup where I can log with event ids as a central part og an API, and what that potentially could lead to of gains

This will _never_ be done, and _shouldn't_ be used for production grade applications. _Not all Log* methods is supported_.

But if you like this, please clone it, or create some pull requests, that provide further enhancement to the API. It could be fun to follow through.

The most important take away with this project is the baseline setup for library development, that you can use or be inspired by.

# Nuget feeds
I have two nuget feeds:

1. A beta nuget feed:  [on Azure DevOps](https://dev.azure.com/servitr/Servitr.LogSink/_packaging?_a=feed&feed=Servitr.LogSink) - called "beta nuget feed". These packages can change a lot from version to version. Expect the API to break!
2. A stable nuget feed: [On github.com](https://github.com/mslot?tab=packages) - called "release nuget feed". These packages here, is stable packages, that has been tested enough in beta. These packages will not change that much from version to version. If they do, I will have warned about it long time before, deprecated the APIs a few releases before. There can be preview/alpha/beta/prerelease (call them what you want) packages here! As long as they are considered stable: that is, as long as it has been battle tested enough

I haven't yet set up a seperate account for my nuget packages, so they are going to be located on my private account on github for now.

# Application Insights
This project is using application insights, but other logging sources could be used. Please note that application insights has no reserved event id numbers, personally I start my count relative high when classifing events: 6000. 

# Examples
I will provide examples on use, and intended use, when the API has been stabilized. If you want examples, clone the repo and look on the unit tests, and the TestConsole.

## Call convention
The call convention might seem a bit more complicated and longer, but please have in mind that we have `EventId` in focus, so there is actually not that much difference:

```csharp
_logger.LogInformation(new EventId(1, "name"), "message {param1} {param2}", "this is parameter 1", "this is parameter 2");
_logSink.LogInformation<ConsoleService>("Log message {param1} {param2}", new string[] { "this is parameter 1", "this is parameter 2" });
```

We are talking about 13 characters. But it still pains my eyes that we have to apply `ConsoleService` and new'ing the log argument list (but this is needed if we are going to get the caller name).