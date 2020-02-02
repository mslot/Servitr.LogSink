using Servitr.LogSink.EventIdMappers.InMemoryEventIdMapper;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using FluentAssertions;

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
                _mapper.AddClassification(className: "classname1", methodName: "methodname1", area: 1, exception: null, 6001, "eventname 1");
                _mapper.AddClassification(className: "classname2", methodName: "methodname2", area: 1, exception: null, 6002, "eventname 2");
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
        }
    }
}
