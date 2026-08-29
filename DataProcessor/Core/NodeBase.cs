namespace DataProcessor.Core;

public abstract class NodeBase<T>
    where T : NodeBase<T>
{
    public string? Value;

    public NodeBase() { }

    public NodeBase(string stepValue)
    {
        Value = stepValue;
    }

    public T? Next
    {
        get;
        protected set
        {
            if (value == null)
            {
                throw new Exception("Empty value is provided");
            }

            field = value;
            field.Previous = (T)this;
        }
    }

    public T? Previous { get; private set; }

    public T? Last
    {
        get
        {
            var currentNode = (T)this;
            while (currentNode.Next != null)
                currentNode = currentNode.Next;

            return currentNode;
        }
    }

    public T? First
    {
        get
        {
            var currentNode = (T)this;
            while (currentNode.Previous != null)
                currentNode = currentNode.Previous;

            return currentNode;
        }
    }
}
