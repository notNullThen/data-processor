using System.Collections.ObjectModel;

namespace DataProcessor.Core;

public class DataProcessor
{
    public DataProcessor(string dataFilePath)
    {
        try
        {
            Lines = File.ReadLines(dataFilePath);
        }
        catch
        {
            throw new FileNotFoundException(
                $"[FILE DOES NOT EXIST] Tried to reach at \"{dataFilePath}\""
            );
        }

        try
        {
            _items = GetItems();
            Items = _items.Select(item => item.Key).OrderBy(itemKey => itemKey);
        }
        catch
        {
            throw new Exception(InvalidFileMessage);
        }
    }

    public const string InvalidFileMessage = "[THE DATA FILE IS INVALID] Please re-check the file.";

    private const string ItemMarker = " Item:";

    public readonly IEnumerable<string> Lines;

    private readonly ReadOnlyDictionary<string, int> _items;

    public readonly IEnumerable<string> Items;

    private ReadOnlyDictionary<string, int> GetItems()
    {
        Dictionary<string, int> items = [];

        for (var i = 0; i < Lines.Count(); i++)
        {
            var line = Lines.ElementAt(i);

            if (line.Contains(ItemMarker))
            {
                var item = line.Split(ItemMarker)[1];
                items.Add(item.Trim(), i);
            }
        }

        return items.AsReadOnly();
    }
}
