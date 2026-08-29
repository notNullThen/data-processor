var dataFilePath = "/home/e-ubuntu/projects/data-processor/DataProcessor/TestData/Data.medium.txt";

var dp = new DataProcessor.Core.DataProcessor(dataFilePath);

Console.WriteLine("Available items:\n");

for (var i = 0; i < dp.Items.Count(); i++)
{
    var item = dp.Items.ElementAt(i);
    Console.WriteLine($"[{i + 1}] - {item}");
}

Console.WriteLine("\nWhat item would you like to search for?");

var itemCount = dp.Items.Count();
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

try
{
    var path = dp.GetItemPath(dp.Items.ElementAt(parsedIndex - 1));

    foreach (var step in path)
        Console.WriteLine($"{step}");
}
catch
{
    throw new Exception(DataProcessor.Core.DataProcessor.InvalidFileMessage);
}
