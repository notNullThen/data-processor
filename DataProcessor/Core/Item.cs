namespace DataProcessor.Core;

public sealed class Item(string? value = null, Item? previous = null, Item? next = null)
{
    public string? Value => value;

    public Item? Previous => previous;

    public Item? Next => next;

    public Item First
    {
        get
        {
            var currentItem = this;
            while (currentItem.Previous != null)
                currentItem = currentItem.Previous;

            return currentItem;
        }
    }

    public Item AddPrevious(string value)
    {
        if (string.IsNullOrEmpty(Value))
        {
            return new(value);
        }

        var item = new Item(value, next: First);

        return item;
    }
}
