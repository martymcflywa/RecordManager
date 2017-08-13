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

            var actual = Generator
                .Get(initSequenceId, 
                     aggregateTypeId, 
                     messageTypeId, 
                     timestamp)
                .Until(limit);

            var path = Directory.GetCurrentDirectory();
            var filename = "Test.dat";
            var filepath = Path.Combine(path, filename);

            using(var stream = new FileStream(filepath, FileMode.Create))
            {
                actual.Write(stream, 0);
            }
        }
    }
}
