using System;
using System.Collections.Generic;
using System.IO;

namespace FileManager
{
    public class FileHandler
    {
        readonly string Path;
        readonly int MaxSize;
        FileStream Stream;
        int FileCount;

        const string FILENAME_FORMAT = "Records_{0}.dat";
        const string PADDING = "D3";

        public FileHandler(string path, int maxSize)
        {
            Path = path;
            MaxSize = maxSize;
            FileCount = 0;

            var filename = GetFilename();
            var filepath = System.IO.Path.Combine(filename, Path);
        }

        public void Write(IEnumerable<Record> records)
        {
            try
            {
                foreach (var record in records)
                {
                    if (IsNewFile(Stream))
                    {
                        Stream = CreateNewFile();
                    }
                    record.Write(Stream);
                }
            }
            finally
            {
                Stream.Dispose();
            }
        }

        bool IsNewFile(FileStream stream)
        {
            return stream == null || stream.Length > MaxSize;
        }

        FileStream CreateNewFile()
        {
            if (Stream != null)
            {
                Stream.Dispose();
            }

            FileCount++;
            var filename = GetFilename();
            var filepath = System.IO.Path.Combine(Path, filename);

            if (!Directory.Exists(Path))
            {
                Directory.CreateDirectory(Path);
            }
            return new FileStream(filepath, FileMode.CreateNew);
        }

        string GetFilename()
        {
            return String.Format(FILENAME_FORMAT, FileCount.ToString(PADDING));
        }
    }
}
