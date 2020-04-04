# Servitr.LogSink
This project is intended as a _pure experiment_ and is put into the world so i can test two things:

1. How to create a yaml DevOps pipeline and nuget repos for library development (and how to use GitVersion properly when branching)
2. To look more into a setup where I can log with event ids as a central part og an API, and what that potentially could lead to of gains

This will _never_ be done, and _shouldn't_ be used for production grade applications. Right now the latest unstable release doesn't support other methods than `LogInformation`. If I have the time, I will expand it to also include `LogError`, `LogFatal` etc (even though this is pretty easy to do yourself when the API is becoming stable). For now the API is unstable, and I will only focus on getting `LogInformation` stable.

But if you like this, please clone it, or create some pull requests, that provide further enhancement to the API. It could be fun to follow through.

# Nuget feeds
I have two nuget feeds:

1. A beta nuget feed:  [on Azure DevOps](https://dev.azure.com/servitr/Servitr.LogSink/_packaging?_a=feed&feed=Servitr.LogSink) - called "beta nuget feed". These packages can change a lot from version to version. Expect the API to break!
2. A stable nuget feed: [On github.com](https://github.com/mslot?tab=packages) - called "release nuget feed". These packages here, is stable packages, that has been tested enough in beta. These packages will not change that much from version to version. If they do, I will have warned about it long time before, deprecated the APIs a few releases before. There can be preview/alpha/beta/prerelease (call them what you want) packages here! As long as they are considered stable: that is, as long as it has been battle tested enough

I haven't yet set up a seperate account for my nuget packages, so they are going to be located on my private account on github for now.

# Application Insights
This project is using application insights, but other logging sources could be used. Please note that application insights has no reserved event id, so I start my count relative high when classifing exceptions, 6000.

It also has a "SimpleLogSink" that uses the logger factory. This could easily be refactored out, but for now I am only focusing on how to make a clean API. 

# Examples
I will provide examples on use, and intended use, when the API has been stabilized. If you want examples, clone the repo and look on the unit tests, and the TestConsole.