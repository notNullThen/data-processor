using System.Text.Json;

namespace DataProcessor.Core;

public class DataProcessor(string filePath)
{
    private readonly IEnumerable<string> Lines = File.ReadLines(filePath);

    private const string ItemMarker = " Item:";
    private const string StepMarker = "+ ";

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

    public List<string> GetPath(int itemLineIndex, int depth)
    {
        var nextDepth = depth - 1;

        List<string> path = [];

        for (var i = itemLineIndex - 1; i >= 0; i--)
        {
            var line = Lines.ElementAt(i);

            if (line.Contains(StepMarker) && GetLineDepth(i, StepMarker) == nextDepth)
            {
                path.Insert(0, line.Split(StepMarker)[1]);
                nextDepth--;
            }
        }

        return path;
    }

    public (int itemLineIndex, int depth) GetItemDepth(string itemName)
    {
        var lineIndex = items[itemName];
        var depth = GetLineDepth(lineIndex, ItemMarker);

        return (lineIndex, depth);
    }

    public int GetLineDepth(int index, string marker)
    {
        var line = Lines.ElementAt(index);
        var lineEndIndex = line.IndexOf(marker);
        var lineDepthPart = line[0..lineEndIndex];
        return lineDepthPart.Split(DepthSamples, StringSplitOptions.None).Length - 1;
    }
}
