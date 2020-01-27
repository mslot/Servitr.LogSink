# Servitr.LogSink
This project is intended to be used as an enchancment to logging. I want to to be able to search intelligently when logging to Application Insights. I want to be using event ids, and I want to be able to map exeptions to event ids and a thing i call areas. This project is an experiment out into the world of logging. The purpose is not to log text, but to auto detect and classify events, for others to query. I want a service/delivery department to be able to set up a monitoring platform that alerts correctly, when certain classification ids is written to log, so they can quickly tune into the problems that matters.

# Application Insights
This project is using application insights, but other logging sources could be used. Please note that application insights has no reserved event id, so I start my count relative high when classifing exceptions, 6000.

# Documentation
Documentation and examples can be found [here](https://logsink.servitr.io).