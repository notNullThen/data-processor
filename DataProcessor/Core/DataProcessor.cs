namespace DataProcessor.Core;

public class DataProcessor(string filePath)
{
    private readonly IEnumerable<string> Content = File.ReadLines(filePath);

    public void ParseItems()
    {
        List<Item> items = [];
        var currentItem = new Item();

        foreach (var line in Content)
        {
            for (var i = 0; i < line.Length; i++)
            {
                var currentChar = line[i];

                if (currentChar.Equals('+'))
                {
                    var step = line[(i + 1)..line.Length];
                    currentItem = currentItem.Add(step.Trim());

                    break;
                }
            }

            if (line.Contains("Item:"))
            {
                var step = line[line.IndexOf("Item:")..line.Length];
                currentItem = currentItem.Add(step.Trim());
                items.Add(currentItem);
                currentItem = new();
            }
        }

        foreach (var item in items)
        {
            item.PrintPath();
        }
    }
}
