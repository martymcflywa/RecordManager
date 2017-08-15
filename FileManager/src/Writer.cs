using System;
using System.IO;

namespace FileManager
{
    public static class Writer
    {
        public static void Write(this Record record, FileStream stream)
        {
            var sequenceIdBytes = BitConverter.GetBytes(record.SequenceId);
            stream.Write(sequenceIdBytes, 0, sequenceIdBytes.Length);

            var aggregateTypeIdByte = BitConverter.GetBytes(record.AggregateTypeId);
            stream.Write(aggregateTypeIdByte, 0, aggregateTypeIdByte.Length);

            var messageTypeIdByte = BitConverter.GetBytes(record.MessageTypeId);
            stream.Write(messageTypeIdByte, 0, messageTypeIdByte.Length);

            var timestampBytes = BitConverter.GetBytes(record.Timestamp);
            stream.Write(timestampBytes, 0, timestampBytes.Length);

            stream.Flush();
        }
    }
}
