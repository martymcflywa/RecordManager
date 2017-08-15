using System.Collections.Generic;

namespace FileManager
{
    public interface IRecordSource
    {
        IEnumerable<Record> Get(int sequenceId, byte aggregateTypeId, byte messageTypeId, long timestamp);
    }
}