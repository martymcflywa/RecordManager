using System.Collections.Generic;

namespace RecordGenerator
{
    public static class Generator
    {
        /// <summary>
        /// Generates a collection of Records. Increments each sequenceId and timestamp from initial seed values.
        /// </summary>
        /// <returns>The collection of generated records.</returns>
        /// <param name="sequenceId">Sequence identifier.</param>
        /// <param name="aggregateTypeId">Aggregate type identifier.</param>
        /// <param name="messageTypeId">Message type identifier.</param>
        public static IEnumerable<Record> Get(int sequenceId, byte aggregateTypeId, byte messageTypeId, long timestamp)
        {
            while (true)
            {
                yield return new Record(sequenceId++, aggregateTypeId, messageTypeId, timestamp++);
            }
        }

        /// <summary>
        /// Controls how many records will be generated.
        /// TODO Might be able to use this to control write limit.
        /// </summary>
        /// <returns>A collection of records of up to count.</returns>
        /// <param name="source">The collection of records to apply the function to.</param>
        /// <param name="count">How many records this function returns.</param>
        public static IEnumerable<Record> Take(this IEnumerable<Record> source, int count)
        {
            using (var enumerator = source.GetEnumerator())
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
