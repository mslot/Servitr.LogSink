# Servitr.LogSink
This project is intended as a _pure experiment_ and is put into the world so i can test two things:

1. How to create a yaml DevOps pipeline, GitHub pages and nuget repos for library development (and how to use GitVersion properly when branching)
2. To look more into a setup where I can log with event ids as a central part og an API, and what that potentially could lead to of gains

This will _never_ be done, and _shouldn't_ be used for production grade applications. _Not all Log* methods is supported_.

But if you like this, please clone it, or create some pull requests, that provide further enhancements to the API. It could be fun to follow through.

The two most important take away with this project is:

1. the baseline setup for library development, that you can use or be inspired by
2. use event ids: it eases the debugging experience and bug hunting for the people doing that in their day to day job. It is much easier to search for event id 5000 (if that event is releated to errors writing a report) than to a random log message eg "give me events with messages containing words "report", and "writing"). Log messages can changes. Event id don't (or shouldn't)

# Application Insights
This project is using application insights, but other logging sources could be used. Please note that application insights has no reserved event id, so I start my count relative high when classifing exceptions, 6000.

## Why the focus on event ids
For people sitting in front line support doing support, it is much easier to search the logs after event id 5000, if they get a bug report (or angry customer) telling that the system is error'ing out when writing a report, than searching through events looking for an event with a message containing the words "writing" and "report". It is also much easier to set up monitoring. The query for Application Insights is so simple:

```kusto
traces 
| where customDimensions.EventId == 5000
| where timestamp > ago(10m)
```

And it wont (or shouldn't) affect if a developer changes the wording of a message. It is an id, and it will not change. I have seen it a lot in the past, where bug hunting is bound to knowing what a log message contains.

# Documentation
Documentation and examples can be found [here](https://logsink.servitr.io).

## Call convention
The call convention might seem a bit more complicated and longer, but please have in mind that we have `EventId` in focus, so there is actually not that much difference:

```csharp
_logger.LogInformation(new EventId(6001, "name"), "message {param1} {param2}", "this is parameter 1", "this is parameter 2");
_logSink.LogInformation<ConsoleService>("Log message {param1} {param2}", new string[] { "this is parameter 1", "this is parameter 2" }, 60);
```

We are talking about 14 characters. But it still pains my eyes that we have to apply `ConsoleService` and new'ing the log argument list (but this is needed if we are going to get the caller name).

# Design of interfaces
I know that the `Log*` interface contains a lot of optionals, and I know it is bad design, but _this_ isn't a production grade library, and my focus isn't on designing a perfect API. My focus is on experminting with event ids, and trying to get a perfect setup for doing library development. Bear over with me. If I in the future want to make a better design, the optionals is the first I am going to look at erasing, and breaking up.