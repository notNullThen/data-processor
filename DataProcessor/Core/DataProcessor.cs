using System.Collections.ObjectModel;

namespace DataProcessor.Core;

public class DataProcessor
{
    public DataProcessor(string dataFilePath)
    {
        _lines = File.ReadLines(dataFilePath);
        _items = GetItems();
    }

    private const string ItemMarker = " Item:";
    private const string StepMarker = "+ ";

    private readonly string[] DepthSamples = ["└──", "├──", "|  ", "   "];
    private readonly IEnumerable<string> _lines;

    private readonly ReadOnlyDictionary<string, int> _items;

    public IEnumerable<string> Items => _items.Select(item => item.Key).AsEnumerable();

    public IEnumerable<string> GetItemPath(string itemName)
    {
        var itemLineIndex = _items[itemName];
        var depth = GetLineDepth(itemLineIndex, ItemMarker);

        var nextDepth = depth - 1;

        List<string> path = [];

        for (var i = itemLineIndex - 1; i >= 0; i--)
        {
            var line = _lines.ElementAt(i);

            if (line.Contains(StepMarker) && GetLineDepth(i, StepMarker) == nextDepth)
            {
                path.Insert(0, line.Split(StepMarker)[1]);
                nextDepth--;
            }
        }

        return path;
    }

    private ReadOnlyDictionary<string, int> GetItems()
    {
        Dictionary<string, int> items = [];

        for (var i = 0; i < _lines.Count(); i++)
        {
            var line = _lines.ElementAt(i);

            if (line.Contains(ItemMarker))
            {
                var item = line.Split(ItemMarker)[1];
                items.Add(item.Trim(), i);
            }
        }

        return items.AsReadOnly();
    }

    private int GetLineDepth(int index, string marker)
    {
        var line = _lines.ElementAt(index);
        var markerStartIndex = line.IndexOf(marker);
        var lineDepthPart = line[0..markerStartIndex];

        // here we can just split by depth length - which is 3
        // but for reliability purposes was decided to use DepthSamples
        return lineDepthPart.Split(DepthSamples, StringSplitOptions.None).Length - 1;
    }
}
