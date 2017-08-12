﻿using System;

namespace RecordGenerator
{
    public class Record
    {
        public int SequenceId { get; }
        public byte AggregateTypeId { get; }
        public byte MessageTypeId { get; }
        public long Timestamp { get; }

        public Record(int sequenceId, byte aggregateTypeId, byte messageTypeId, long timestamp)
        {
            SequenceId = sequenceId;
            AggregateTypeId = aggregateTypeId;
            MessageTypeId = messageTypeId;
            Timestamp = timestamp;
        }
    }
}
