var dataFilePath = "/home/e-ubuntu/projects/data-processor/DataProcessor/TestData/Data.medium.txt";

var dp = new DataProcessor.Core.DataProcessor(dataFilePath);

Console.WriteLine("Available items:\n");

for (var i = 0; i < dp.Items.Count(); i++)
{
    var item = dp.Items.ElementAt(i);
    Console.WriteLine($"[{i + 1}] - {item}");
}

Console.WriteLine("\nWhat item would you like to search for?");

var userInput = string.Empty;
var parseSucceded = false;
int parsedIndex = 0;

while (string.IsNullOrWhiteSpace(userInput) || !parseSucceded)
{
    Console.WriteLine("Please enter proper number");
    userInput = Console.ReadLine();
    parseSucceded = int.TryParse(userInput, out var itemIndex);
    parsedIndex = itemIndex;
}

Console.WriteLine();

var path = dp.GetItemPath(dp.Items.ElementAt(parsedIndex - 1));

foreach (var step in path)
    Console.WriteLine($"{step}");
