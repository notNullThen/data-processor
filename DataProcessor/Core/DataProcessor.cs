using System.Collections.ObjectModel;

namespace DataProcessor.Core;

public class DataProcessor
{
    public DataProcessor(string[] lines)
    {
        _lines = lines;

        _indexedItemsLines = ParseItems();
        Items = [.. _indexedItemsLines.Select(item => item.Key).OrderBy(itemKey => itemKey)];
    }

    public const string InvalidFileMessage = "[THE DATA FILE IS INVALID] Please re-check the file.";

    private const string ItemMarker = " Item:";
    private const string StepMarker = "+ ";
    private const int IdentLength = 3;

    private readonly string[] _lines;

    private readonly ReadOnlyDictionary<string, int> _indexedItemsLines;

    public string[] Items { get; }

    public Item GetItemByName(string itemName)
    {
        var itemLineIndex = _indexedItemsLines[itemName];
        var depth = GetLineDepth(itemLineIndex, ItemMarker);

        var nextDepth = depth - 1;

        var item = new Item();

        for (var i = itemLineIndex - 1; i >= 0; i--)
        {
            var line = _lines[i];

            ValidateLine(line, i);

            if (line.Contains(StepMarker) && GetLineDepth(i, StepMarker) == nextDepth)
            {
                item = item.AddPrevious(line.Split(StepMarker)[1]);
                nextDepth--;
            }
        }

        return item.First;
    }

    private ReadOnlyDictionary<string, int> ParseItems()
    {
        Dictionary<string, int> items = [];

        for (var i = 0; i < _lines.Length; i++)
        {
            var line = _lines[i];

            ValidateLine(line, i);

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
        var line = _lines[index];

        var lineDepthLength = line.IndexOf(marker);
        if (!(lineDepthLength % 3 == 0))
            throw new InvalidDataException(
                $"[INVALID LINE] Line #{index + 1} has incorrect identation length {IdentLength}. Line content is below:\n{line}\n"
            );

        return lineDepthLength / IdentLength;
    }

    private void ValidateLine(string line, int lineIndex)
    {
        if (!line.Contains(StepMarker) && !line.Contains(ItemMarker))
        {
            throw new FileLoadException(
                $"[INVALID LINE] Line #{lineIndex + 1} contains neither the \"{StepMarker}\" step marker nor the \"{ItemMarker}\" item marker. Line content is below:\n{line}\n"
            );
        }
    }
}
