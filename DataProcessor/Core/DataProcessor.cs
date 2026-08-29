using System.Text.Json;

namespace DataProcessor.Core;

public class DataProcessor(string filePath)
{
    private readonly IEnumerable<string> Lines = File.ReadLines(filePath);

    private const string ItemMarker = "Item:";

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

        Console.WriteLine(JsonSerializer.Serialize(items));
    }
}
