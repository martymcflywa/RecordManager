namespace FileManager
{
    public static class RecordStructure
    {
        public const int SEQUENCE_ID = 4;
        public const int AGGREGATE_TYPE_ID = 2;
        public const int MESSAGE_TYPE_ID = 2;
        public const int TIMESTAMP = 8;
        public const int LENGTH = SEQUENCE_ID + AGGREGATE_TYPE_ID + MESSAGE_TYPE_ID + TIMESTAMP;
    }
}
