using ConsoleUi = DataProcessor.ConsoleHandler.ConsoleUi;

if (args.Length <= 0)
    throw new ArgumentException(
        "[FILE PATH NOT PROVIDED] Please re-check file path argument you passed."
    );

var dataFilePath = args[0];

var lines = File.ReadAllLines(dataFilePath);

var dataProcessor = new DataProcessor.Core.DataProcessorCore(lines);

ConsoleUi.PrintItems(dataProcessor.OrderedItems);

int parsedIndex = ConsoleUi.ReadItemIndex(dataProcessor.OrderedItems.Length);

var path = dataProcessor.GetItemPath(dataProcessor.OrderedItems[parsedIndex]);

ConsoleUi.PrintItemPath(path);
