using DataProcessor.Core;

namespace DataProcessor.ConsoleHandler;

public static class ConsoleUi
{
    public static void PrintItems(string[] items)
    {
        var consoleItemsList = ConsoleFormatter.FormatItemsList(items);

        Console.WriteLine("Available items:\n");
        foreach (var item in consoleItemsList)
            Console.WriteLine(item);
    }

    public static int ReadItemIndex(int orderedItemsLength)
    {
        Console.WriteLine("\nWhat item would you like to search for?");

        var userInput = Console.ReadLine();

        int parsedItemNumber;
        while (
            !ConsoleFormatter.TryParseItemNumber(
                orderedItemsLength,
                userInput,
                out parsedItemNumber
            )
        )
        {
            Console.WriteLine("Please enter valid item number");
            userInput = Console.ReadLine();
        }

        Console.WriteLine();

        return parsedItemNumber - 1;
    }

    public static void PrintItemPath(Item item)
    {
        foreach (var step in ConsoleFormatter.GetItemPath(item))
            Console.WriteLine($"{step}");
    }
}
