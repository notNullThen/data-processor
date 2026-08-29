using System.Text.Json;

namespace DataProcessor.Core;

public class DataProcessor(string filePath)
{
    private readonly IEnumerable<string> Lines = File.ReadLines(filePath);

    private const string ItemMarker = " Item:";
    private const int DepthLength = 3;
    private readonly string[] DepthSamples = ["└──", "├──", "|  ", "   "];

    private readonly Dictionary<string, int> items = [];

    public void GetItems()
    {
        for (var i = 0; i < Lines.Count(); i++)
        {
            var line = Lines.ElementAt(i);

            if (line.Contains(ItemMarker))
            {
                var item = line.Split(ItemMarker)[1];
                items.Add(item.Trim(), i);
            }
        }
    }

    public int GetItemDepth(string itemName)
    {
        var itemIndex = items[itemName];
        var itemLine = Lines.ElementAt(itemIndex);
        var itemDepthPart = itemLine[0..itemLine.IndexOf(ItemMarker)];

        return itemDepthPart.Split(DepthSamples, StringSplitOptions.None).Length - 1;
    }
}
