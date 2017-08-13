﻿using System;
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

			foreach(var record in records)
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
            }
        }

		public static IEnumerable<Record> Limit(this IEnumerable<Record> records, int count)
		{
			using (var enumerator = records.GetEnumerator())
			{
				var i = 0;
				while (enumerator.MoveNext() && i++ < count)
				{
					yield return enumerator.Current;
				}
			}
		}
    }
}
