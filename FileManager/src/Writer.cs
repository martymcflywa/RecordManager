using System;
using System.Collections.Generic;
using System.IO;
using RecordGenerator;

namespace FileManager
{
    public static class Writer
    {
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

        public static bool IsNewFileRequired(FileStream stream, int maxSize)
        {
            return stream.Length > maxSize;
        }

        public static FileStream CreateNewFile(
            FileStream stream,
            string path,
            string filenameFormat,
            string zeroPad,
            int currentFileCount)
        {
            stream.Dispose();
            var filename = String.Format(filenameFormat, (currentFileCount + 1).ToString(zeroPad));
            var filepath = Path.Combine(path, filename);
            return new FileStream(filepath, FileMode.Create);
        }
    }
}
