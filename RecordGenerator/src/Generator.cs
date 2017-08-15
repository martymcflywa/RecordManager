using System.Collections.Generic;
using FileManager;

namespace RecordGenerator
{
    public class Generator : IRecordSource
    {
        /// <summary>
        /// Generates a collection of Records. Increments each sequenceId and timestamp from initial seed values.
        /// </summary>
        /// <returns>The collection of generated records.</returns>
        /// <param name="sequenceId">Sequence identifier.</param>
        /// <param name="aggregateTypeId">Aggregate type identifier.</param>
        /// <param name="messageTypeId">Message type identifier.</param>
        public IEnumerable<Record> Get(int sequenceId, byte aggregateTypeId, byte messageTypeId, long timestamp)
        {
            while (true)
            {
                yield return new Record(sequenceId++, aggregateTypeId, messageTypeId, timestamp++);
            }
        }
    }
}
