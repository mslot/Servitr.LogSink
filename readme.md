# Servitr.LogSink
This project is intended as a _pure experiment_ and is put into the world so i can test two things:

1. How to create a yaml DevOps pipeline, GitHub pages and nuget repos for library development (and how to use GitVersion properly when branching)
2. To look more into a setup where I can log with event ids as a central part og an API, and what that potentially could lead to of gains

This will _never_ be done, and _shouldn't_ be used for production grade applications. Right now the latest unstable release doesn't support other methods than `LogInformation`. If I have the time, I will expand it to also include `LogError`, `LogFatal` etc (even though this is pretty easy to do yourself when the API is becoming stable). For now the API is unstable, and I will only focus on getting `LogInformation` stable.

But if you like this, please clone it, or create some pull requests, that provide further enhancement to the API. It could be fun to follow through.

The most important take away with this project is the baseline setup for library development, that you can use or be inspired by.

# Application Insights
This project is using application insights, but other logging sources could be used. Please note that application insights has no reserved event id, so I start my count relative high when classifing exceptions, 6000.

# Documentation
Documentation and examples can be found [here](https://logsink.servitr.io).

## Call convention
The call convention might seem a bit more complicated and longer, but please have in mind that we have `EventId` in focus, so there is actually not that much difference:

```csharp
_logger.LogInformation(new EventId(6001, "name"), "message {param1} {param2}", "this is parameter 1", "this is parameter 2");
_logSink.LogInformation<ConsoleService>("Log message {param1} {param2}", new string[] { "this is parameter 1", "this is parameter 2" }, 60);
```

We are talking about 14 characters. But it still pains my eyes that we have to apply `ConsoleService` and new'ing the log argument list (but this is needed if we are going to get the caller name).