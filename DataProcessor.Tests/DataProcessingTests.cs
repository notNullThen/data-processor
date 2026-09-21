using DataProcessor.Core;
using DataProcessor.Tests.TestData;

namespace DataProcessor.Tests;

public class DataProcessingTests
{
    public static TheoryData<string[]> ValidDataSets =>
        [DataMedium.Value, DataDifferentIdent.Value];

    public static TheoryData<string[]> InvalidData => [DataCorrupted.Value];

    [Theory]
    [MemberData(nameof(ValidDataSets))]
    public void FindsCorrectPathToDeeplyNestedItem(string[] lines)
    {
        string[] expectedPath =
        [
            "Enter the building lobby.",
            "Go to the east hallway.",
            "Enter the bedroom.",
            "Check under the bed.",
        ];

        var dp = new DataProcessorCore(lines);

        var actualPath = dp.GetItemPath("Shoe Box");

        Assert.Equal(expectedPath, actualPath);
    }

    [Theory]
    [MemberData(nameof(InvalidData))]
    public void ThrowsErrorWhenReadingCorruptedFile(string[] lines)
    {
        Assert.Throws<InvalidDataException>(() =>
        {
            new DataProcessorCore(lines);
        });
    }
}
