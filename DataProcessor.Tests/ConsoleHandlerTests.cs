using DataProcessor.ConsoleHandler;

namespace DataProcessor.Tests;

public class ConsoleHandlerTests
{
    public static TheoryData<string, int, bool, int> ParseData =>
        [
            // Boundary values testing
            ("1", 1, true, 1),
            ("3", 2, false, 3),
            // Zero shouldn't parse
            ("0", 1, false, 0),
            // Float shouldn't parse
            ("0.1", 1, false, 0),
            ("1.0", 1, false, 0),
            // Symbols & letters shouldn't parse
            ("!@#$", 1, false, 0),
            ("Three", 3, false, 0),
        ];

    [Theory]
    [MemberData(nameof(ParseData))]
    public void ItemIndexIsReadCorrectly(
        string userInput,
        int itemsLength,
        bool expectedSucceed,
        int expectedIndex
    )
    {
        var actualSucceded = ConsoleFormatter.TryParseItemNumber(
            itemsLength,
            userInput,
            out int actualIndex
        );

        Assert.Equal(expectedSucceed, actualSucceded);
        Assert.Equal(expectedIndex, actualIndex);
    }
}
