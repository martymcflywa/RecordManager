# Lee's mini challenge

## Acceptance requirements:

1. Write multiple fixed length records as **bytes** to file
    - **Record:** composed of multiple fixed length elements
    - Example: `[(int) SequenceId][(Guid) AggregateTypeId][(MessageTypeId)][(int) Timestamp]`
2. Writer must stop when some limit is reached
    - Could be
        - File size
        - Record count
3. Filenames should be sequential
    - Could contain first and last `SequenceId`
    - Example: `Records_001_100.dat`, `Records_101_200.dat`, `Records_201_300.dat`
4. Must be able to interpret the bytes from file back to string
    - Validate against dataset, either known or deterministic