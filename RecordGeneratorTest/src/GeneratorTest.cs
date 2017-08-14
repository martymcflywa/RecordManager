using System;
using System.Collections.Generic;
using RecordGenerator;
using Xunit;

namespace RecordGeneratorTest
{
    public class GeneratorTest
    {
        [Fact]
        public void Get()
        {
            var limit = 10;
            var initSequenceId = 1;
            var aggregateTypeId = (byte)5;
            var messageTypeId = (byte)10;
            var timestamp = DateTimeOffset.Now.ToUnixTimeMilliseconds();

            var actual = Generator
                .Get(initSequenceId,
                     aggregateTypeId,
                     messageTypeId,
                     timestamp)
                .Take(limit);

            Assert.NotEmpty(actual);

            var list = new List<RecordGenerator.Record>(actual);
            Assert.True(list.Count == limit);

            foreach (var item in actual)
            {
                Assert.Equal(initSequenceId++, item.SequenceId);
                Assert.Equal(aggregateTypeId, item.AggregateTypeId);
                Assert.Equal(messageTypeId, item.MessageTypeId);
                Assert.Equal(timestamp++, item.Timestamp);
            }
        }
    }
}
