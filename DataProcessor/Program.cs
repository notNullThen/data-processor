using AIOrchestrator.Core;
using DataProcessor.AiCore;

const string modelName = "ministral-3:3b";

if (args.Length <= 0)
    throw new ArgumentException(
        "[FILE PATH NOT PROVIDED] Please re-check file path argument you passed."
    );

var dataFilePath = args[0];

var dp = new DataProcessor.Core.DataProcessor(dataFilePath);
var ai = new AiManager(
    modelName,
    appInstance: new AiFacade(dp),
    options: new() { Temperature = 0 }
);

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

var selectedItem = dp.Items.ElementAt(parsedIndex - 1);
await ai.StartAsync(selectedItem);
