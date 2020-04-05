using Servitr.LogSink.EventIdMappers.InMemoryEventIdMapper;
using Xunit;
using FluentAssertions;
using System;

namespace Servitr.LogSink.Tests.Unit.InMemoeryEventIdMapperClassTest
{
    public class InMemoryEventIdMapperClass
    {
        public class GetEventClassificationMethod
        {
            private InMemoryEventIdMapper _mapper;
            public GetEventClassificationMethod()
            {
                _mapper = new InMemoryEventIdMapper();

                //Without exceptions
                _mapper.AddClassification(className: "classname1", methodName: "methodname1", areaId: 1, 
                    areaName:"area", exceptionType: null, 6001, "eventname 1");
                _mapper.AddClassification(className: "classname2", methodName: "methodname2", areaId: 1, 
                    areaName:"area", exceptionType: null, 6002, "eventname 2");

                //With exceptions
                _mapper.AddClassification(className: "classname3", methodName: "methodname3", areaId: 1, 
                    areaName:"area", exceptionType: typeof(Exception), 6002, "eventname 3");
            }
            [Fact]
            public void SimpleCall()
            {
                var classification = _mapper.GetEventClassification("classname1", "methodname1", 1, null);
                
                classification
                    .Should()
                    .NotBeNull();

                classification
                    .Name
                    .Should()
                    .Be("eventname 1");

                classification
                    .EventId
                    .Should()
                    .Be(6001);
            }

            [Fact]
            public void GetEventWithException()
            {
                var classification = _mapper.GetEventClassification("classname3", "methodname3", 1, typeof(Exception));

                classification
                    .Should()
                    .NotBeNull();

                classification
                    .Name
                    .Should()
                    .Be("eventname 3");

                classification
                    .EventId
                    .Should()
                    .Be(6002);
            }
        }
    }
}
