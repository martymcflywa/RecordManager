using System;
using System.Collections.Generic;
using System.IO;
using RecordGenerator;

namespace FileManager
{
    public static class Writer
    {
        public static void Write(this IEnumerable<Record> records, FileStream stream, int maxSize, string path, string filenameFormat, string zeroPad)
        {
            try
            {
                var fileCount = 0;
                foreach (var record in records)
                {
                    if(IsNewFileRequired(stream, maxSize))
                    {
                        stream = CreateNewFile(stream, path, filenameFormat, zeroPad, fileCount);
                        fileCount++;
                    }
                    record.Write(stream);
                }
            }
            finally
            {
                stream.Dispose();
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
