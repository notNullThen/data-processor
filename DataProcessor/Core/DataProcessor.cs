namespace DataProcessor.Core;

public class DataProcessor(string filePath)
{
    private const string ItemStart = "Item:";
    private const char StepStart = '+';
    private const char Filler = '─';

    private readonly char Separator = '|';
    private readonly char[] NextStepSeparators = ['├', '└'];

    private string FileContent = File.ReadAllText(filePath);

    public void ParseItems()
    {
        var steps = new Step();

        bool stepStarted = false;

        var currentStep = string.Empty;

        foreach (var currentChar in FileContent)
        {
            if (currentChar.Equals(Filler))
                continue;

            // Start step
            if (currentChar.Equals(StepStart))
            {
                stepStarted = true;
                continue;
            }

            // End the step
            if (currentChar.Equals(Separator) || NextStepSeparators.Contains(currentChar))
            {
                var parsedStep = currentStep.Trim();

                if (!string.IsNullOrWhiteSpace(parsedStep))
                    steps = steps.Add(parsedStep);

                currentStep = string.Empty;
                stepStarted = false;
                continue;
            }

            if (stepStarted)
            {
                currentStep += currentChar;
            }
        }

        steps = steps.First;
    }
}
