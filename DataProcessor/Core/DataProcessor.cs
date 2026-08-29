namespace DataProcessor.Core;

public class DataProcessor(string filePath)
{
    private readonly IEnumerable<string> Lines = File.ReadLines(filePath);

    private const string ItemMarker = "Item:";

    public IEnumerable<string> GetItems()
    {
        List<string> items = [];

        foreach (var line in Lines)
        {
            if (line.Contains(ItemMarker))
            {
                var item = line.Split(ItemMarker)[1];
                items.Add(item.Trim());
            }
        }

        return items;
    }
}
