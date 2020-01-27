# Servitr.LogSink
This project is intended to be used as an enchancment to logging. I want to to be able to search intelligently when logging to Application Insights. I want to be using event ids, and I want to be able to map exeptions to event ids and a thing i call areas. This project is an experiment out into the world of logging.

# Nuget feeds
I have two nuget feeds:

1. A beta nuget feed:  [on Azure DevOps](https://dev.azure.com/servitr/Servitr.LogSink/_packaging?_a=feed&feed=Servitr.LogSink) - called "beta nuget feed". These packages can change a lot from version to version. Expect the API to break!
2. A stable nuget feed: [On github.com](https://github.com/mslot?tab=packages) - called "release nuget feed". These packages here, is stable packages, that has been tested enough in beta. These packages will not change that much from version to version. If they do, I will have warned about it long time before, deprecated the APIs a few releases before. There can be preview/alpha/beta/prerelease (call them what you want) packages here! As long as they are considered stable: that is, as long as it has been battle tested enough

I haven't yet set up a seperate account for my nuget packages, so they are going to be located on my private account on github for now.

_PLEASE_ note that the 0.1.0 release is a true experimental release that's only purpose is to create a proper release setup. Version 0.2.0 will be true beta release that you can use. I still need to do some logic rewriting in the SimpleLogSink (the LogInformation and LogError methods).

# Application Insights
This project is using application insights, but other logging sources could be used. Please note that application insights has no reserved event id, so I start my count relative high when classifing exceptions, 6000.

# More to come here
When I get the last bits and pieces stitched together I am going to make some examples of use with references to how I use it with my blog.