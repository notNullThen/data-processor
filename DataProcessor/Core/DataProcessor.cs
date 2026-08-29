namespace DataProcessor.Core;

public class DataProcessor(string filePath)
{
    private const string ItemStart = "Item:";
    private const char NextStep = '└';
    private const char Branch = '├';
    private const char LineStart = '|';

    private const char StepStart = '+';
    private readonly char[] Fillers = ['─', LineStart];

    private readonly IEnumerable<string> Content = File.ReadLines(filePath);

    public void ParseItems()
    {
        Item item = new();

        foreach (var line in Content)
        {
            for (var i = 0; i < line.Length; i++)
            {
                var currentChar = line[i];

                if (currentChar.Equals('+'))
                {
                    var step = line[(i + 1)..line.Length];
                    item = item.Add(step.Trim());
                    break;
                }
            }
        }
    }
}
