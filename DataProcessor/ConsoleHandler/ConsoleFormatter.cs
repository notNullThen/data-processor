namespace DataProcessor.ConsoleHandler;

public static class ConsoleFormatter
{
    public static string[] FormatItemsList(string[] items) =>
        [.. items.Select((item, index) => $"{index + 1} - {item}")];

    public static bool TryParseItemNumber(int itemsLength, string? userInput, out int parsedNumber)
    {
        var parseSucceded = int.TryParse(userInput, out parsedNumber);

        if (string.IsNullOrWhiteSpace(userInput))
            return false;

        if (!(parsedNumber >= 1 && parsedNumber <= itemsLength))
        {
            return false;
        }

        return parseSucceded;
    }
}
