using DataProcessor.Core;
using DataProcessor.Tests.TestData;

namespace DataProcessor.Tests;

public class DataProcessingTests
{
    public static TheoryData<string, string[]> ItemsPaths =>
        [
            (
                "Granola Bars",
                [
                    "Enter the building lobby.",
                    "Go to the west hallway.",
                    "Enter the kitchen.",
                    "Open the pantry.",
                ]
            ),
            (
                "Orange Juice",
                [
                    "Enter the building lobby.",
                    "Go to the west hallway.",
                    "Enter the kitchen.",
                    "Open the fridge.",
                ]
            ),
            (
                "Shoe Box",
                [
                    "Enter the building lobby.",
                    "Go to the east hallway.",
                    "Enter the bedroom.",
                    "Check under the bed.",
                ]
            ),
        ];

    public static TheoryData<string[]> InvalidData => [DataCorrupted.Value];

    [Theory]
    [MemberData(nameof(ItemsPaths))]
    public void FindPaths(string itemName, string[] expectedPath)
    {
        var lines = DataMedium.Value;
        var dp = new DataProcessorCore(lines);

        var actualPath = dp.GetItemPath(itemName);

        Assert.Equal(expectedPath, actualPath);
    }

    [Theory]
    [MemberData(nameof(ItemsPaths))]
    public void FindDifferentIdentPaths(string itemName, string[] expectedPath)
    {
        var lines = DataDifferentIdent.Value;
        var dp = new DataProcessorCore(lines);

        var actualPath = dp.GetItemPath(itemName);

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
