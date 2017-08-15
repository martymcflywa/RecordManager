using System;
using System.IO;
using System.Linq;
using FileManager;
using RecordGenerator;
using Xunit;

namespace FileManagerTest
{
    public class FileHandlerTest
    {

        [Fact]
        public void Write()
        {
            var recordLimit = 1000;
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

            var path = Path.Combine(Directory.GetCurrentDirectory(), "test");
            var maxSize = 1000;

            var fileHandler = new FileHandler(path, maxSize);
            fileHandler.Write(records);
            fileHandler = null;

            var actualFileCount = 0;
            var files = Directory.GetFiles(path);
            foreach (var file in files)
            {
                var fileInfo = new FileInfo(file);
                Assert.True(file.Length <= maxSize);
                Assert.Equal(".dat", fileInfo.Extension);
                actualFileCount++;
            }
            var expectedFileCount = Math.Ceiling((double)(recordLimit * RecordStructure.LENGTH) / maxSize);
            Assert.Equal(expectedFileCount, actualFileCount);
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
