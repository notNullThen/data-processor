namespace DataProcessor.Core;

public sealed class Item(string? value = null, Item? next = null)
{
    public string? Value => value;

    public Item? Next => next;

    public Item Add(string value)
    {
        if (string.IsNullOrEmpty(Value))
        {
            return new(value);
        }

        return new Item(value: Value, next: Next == null ? new Item(value) : Next.Add(value));
    }

    public void PrintPath()
    {
        List<string> path = [Value!];

        var currentItem = this;

        while (currentItem.Next != null)
        {
            currentItem = currentItem.Next;
            path.Add(currentItem.Value!);
        }

        Console.WriteLine(string.Join(" > ", path));
    }
}
