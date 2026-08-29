var dataFilePath = "/home/e-ubuntu/projects/data-processor/DataProcessor/TestData/Data.medium.txt";

var dp = new DataProcessor.Core.DataProcessor(dataFilePath);

var path = dp.GetItemPath("Allen Wrench Set");
