namespace DataProcessor.Core;

public class Step : NodeBase<Step>
{
    public Step()
        : base() { }

    public Step(string stepValue)
        : base(stepValue) { }

    public Step Add(string value)
    {
        if (string.IsNullOrWhiteSpace(Value))
        {
            Value = value;
            return this;
        }
        else
        {
            Next = new(value);
            return Next;
        }
    }
}
