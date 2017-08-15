using System;
using System.IO;
using System.Linq;
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
            var recordLimit = 5;
            var initSequenceId = 1;
            var aggregateTypeId = (byte)20;
            var messageTypeId = (byte)13;
            var timestamp = DateTimeOffset.Now.ToUnixTimeMilliseconds();
            var generator = new Generator();

            var records = generator
                .Get(initSequenceId,
                     aggregateTypeId,
                     messageTypeId,
                     timestamp)
                .Take(recordLimit);

            var filenameFormat = "Records_{0}.dat";
            var zeroPad = "D3";

            var fileCount = 1;

            var path = Path.Combine(Directory.GetCurrentDirectory(), "test");
            Directory.CreateDirectory(path);
            var filename = String.Format(filenameFormat, (fileCount).ToString(zeroPad));
            var filepath = Path.Combine(path, filename);

            using (var stream = new FileStream(filepath, FileMode.CreateNew))
            {
                foreach (var record in records)
                {
                    record.Write(stream);
                }
            }

            var expectedSize = recordLimit * RecordStructure.LENGTH;

            Assert.True(File.Exists(filepath));
            Assert.Equal(expectedSize, new FileInfo(filepath).Length);
            Teardown(path);
        }

        void Teardown(string path)
        {
            var files = Directory.GetFiles(path);
            foreach (var file in files)
            {
                File.Delete(file);
            }
            Directory.Delete(path);
        }
    }
}
