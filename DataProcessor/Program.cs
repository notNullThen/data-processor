var dp = new DataProcessor.Core.DataProcessor(
    "/home/e-ubuntu/projects/data-processor/DataProcessor/TestData/Data.small.txt"
);

Console.WriteLine("File Content:\n");
dp.ParseItems();
