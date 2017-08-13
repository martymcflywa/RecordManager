using System;
using System.Collections.Generic;
using System.IO;
using RecordGenerator;

namespace FileManager
{
    public static class Writer
    {
        static string FILENAME_FORMAT = "Records_{0}_{1}.dat";
        static string ZERO_PADDING = "D3";

        public static void Write(this IEnumerable<Record> records, FileStream stream, int offset)
        {
            var _bytes = new byte[RecordStructure.LENGTH];
            var _offset = offset;

            foreach (var record in records)
            {
                var sequenceIdBytes = BitConverter.GetBytes(record.SequenceId);
                stream.Write(sequenceIdBytes, 0, sequenceIdBytes.Length);
                _offset += sequenceIdBytes.Length - 1;

                var aggregateTypeIdByte = BitConverter.GetBytes(record.AggregateTypeId);
                stream.Write(aggregateTypeIdByte, 0, aggregateTypeIdByte.Length);
                _offset += aggregateTypeIdByte.Length - 1;

                var messageTypeIdByte = BitConverter.GetBytes(record.MessageTypeId);
                stream.Write(messageTypeIdByte, 0, messageTypeIdByte.Length);
                _offset += messageTypeIdByte.Length - 1;

                var timestampBytes = BitConverter.GetBytes(record.Timestamp);
                stream.Write(timestampBytes, 0, timestampBytes.Length);
                _offset += timestampBytes.Length - 1;

                stream.Flush();
            }
        }

        public static int Write(this Record record, FileStream stream)
        {
            var length = 0;

            var sequenceIdBytes = BitConverter.GetBytes(record.SequenceId);
            stream.Write(sequenceIdBytes, 0, sequenceIdBytes.Length);
            length += sequenceIdBytes.Length - 1;

            var aggregateTypeIdByte = BitConverter.GetBytes(record.AggregateTypeId);
            stream.Write(aggregateTypeIdByte, 0, aggregateTypeIdByte.Length);
            length += aggregateTypeIdByte.Length - 1;

            var messageTypeIdByte = BitConverter.GetBytes(record.MessageTypeId);
            stream.Write(messageTypeIdByte, 0, messageTypeIdByte.Length);
            length += messageTypeIdByte.Length - 1;

            var timestampBytes = BitConverter.GetBytes(record.Timestamp);
            stream.Write(timestampBytes, 0, timestampBytes.Length);
            length += timestampBytes.Length - 1;

            stream.Flush();
            return length;
        }

        public static bool IsNewFileRequired(FileStream stream, int maxSize)
        {
            return stream.Length > maxSize;
        }
    }
}
