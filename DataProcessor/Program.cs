using System.Text.Json;

var dp = new DataProcessor.Core.DataProcessor(
    "/home/e-ubuntu/projects/data-processor/DataProcessor/TestData/Data.medium.txt"
);

Console.WriteLine(JsonSerializer.Serialize(dp.GetItems()));
