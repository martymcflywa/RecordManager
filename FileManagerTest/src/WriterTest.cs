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
        public void Write()
        {
            var limit = 50;
            var initSequenceId = 1;
            var aggregateTypeId = (byte)5;
            var messageTypeId = (byte)10;
            var timestamp = DateTimeOffset.Now.ToUnixTimeMilliseconds();

            var records = Generator
                .Get(initSequenceId,
                     aggregateTypeId,
                     messageTypeId,
                     timestamp)
                .Take(limit);

            var path = Directory.GetCurrentDirectory();
            var filename = "Test.dat";
            var filepath = Path.Combine(path, filename);

            using (var stream = new FileStream(filepath, FileMode.Create))
            {
                records.Write(stream, 0);
            }

            Assert.True(File.Exists(filepath));
            var expectedFilesize = RecordStructure.LENGTH * limit;
            var fileInfo = new FileInfo(filepath);
            Assert.Equal(expectedFilesize, fileInfo.Length);
            File.Delete(filepath);
        }

        [Fact]
        public void WriteMultipleFiles()
        {
            var recordLimit = 50;
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
            var zeroPadding = "D3";

            var maxFileSize = 100;
            var currentFileCount = 0;

            var path = Directory.GetCurrentDirectory();
            var filename = String.Format(filenameFormat, (currentFileCount + 1).ToString(zeroPadding));
            var filepath = Path.Combine(path, filename);
            var stream = new FileStream(filepath, FileMode.Create);

            try
            {
                foreach (var record in records)
                {
                    if (Writer.IsNewFileRequired(stream, maxFileSize))
                    {
                        stream = Writer.CreateNewFile(stream, path, filenameFormat, zeroPadding, currentFileCount);
                        currentFileCount++;
                    }
                    record.Write(stream);
                }
            }
            finally
            {
                stream.Dispose();
            }
        }
    }
}
