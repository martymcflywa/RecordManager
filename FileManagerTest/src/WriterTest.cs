using System;
using System.IO;
using FileManager;
using RecordGenerator;
using Xunit;

namespace FileManagerTest
{
    public class WriterTest
    {
        [Fact]
        public void WriteMultipleFiles()
        {
            var recordLimit = 5000;
            var initSequenceId = 1;
            var aggregateTypeId = (byte)20;
            var messageTypeId = (byte)13;
            var timestamp = DateTimeOffset.Now.ToUnixTimeMilliseconds();

            var records = Generator
                .Get(initSequenceId,
                     aggregateTypeId,
                     messageTypeId,
                     timestamp)
                .Take(recordLimit);

            var filenameFormat = "Records_{0}.dat";
            var zeroPad = "D3";

            var maxSize = 1000;
            var currentFileCount = 0;

            var path = Directory.GetCurrentDirectory();
            var filename = String.Format(filenameFormat, (currentFileCount + 1).ToString(zeroPad));
            var filepath = Path.Combine(path, filename);
            var stream = new FileStream(filepath, FileMode.Create);

            records.Write(stream, maxSize, path, filenameFormat, zeroPad);
        }
    }
}
