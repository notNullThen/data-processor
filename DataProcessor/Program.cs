if (args.Length <= 0)
    throw new ArgumentException(
        "[FILE PATH NOT PROVIDED] Please re-check file path argument you passed."
    );

var dataFilePath = args[0];

string[] lines = [];
try
{
    lines = File.ReadAllLines(dataFilePath);
}
catch
{
    throw new FileNotFoundException($"[FILE DOES NOT EXIST] Tried to reach at \"{dataFilePath}\"");
}

var dp = new DataProcessor.Core.DataProcessor(lines);

Console.WriteLine("Available items:\n");

for (var i = 0; i < dp.Items.Length; i++)
{
    var itemName = dp.Items[i];
    Console.WriteLine($"[{i + 1}] - {itemName}");
}

Console.WriteLine("\nWhat item would you like to search for?");

var itemCount = dp.Items.Length;
int parsedIndex;

while (true)
{
    var userInput = Console.ReadLine();
    var parseSucceded = int.TryParse(userInput, out parsedIndex);

    if (parseSucceded && parsedIndex >= 1 && parsedIndex <= itemCount)
    {
        break;
    }

    Console.WriteLine("Please enter valid number");
}

Console.WriteLine();

var item = dp.GetItem(dp.Items[parsedIndex - 1]);

while (item != null)
{
    Console.WriteLine($"{item.Value}");
    item = item.Next;
}
