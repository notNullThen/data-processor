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
    var item = dp.Items[i];
    Console.WriteLine($"[{i + 1}] - {item}");
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

var path = dp.GetItemPath(dp.Items[parsedIndex - 1]);

foreach (var step in path)
    Console.WriteLine($"{step}");
