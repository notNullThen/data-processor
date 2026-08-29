using System.Text.Json;

var dp = new DataProcessor.Core.DataProcessor(
    "/home/e-ubuntu/projects/data-processor/DataProcessor/TestData/Data.medium.txt"
);

dp.GetItems();
var (itemLineIndex, depth) = dp.GetItemDepth("Allen Wrench Set");
var path = dp.GetPath(itemLineIndex, depth);
Console.WriteLine();
