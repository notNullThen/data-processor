namespace DataProcessor.AiCore;

using AIOrchestrator.Core.AiAppFacade;
using AIOrchestrator.Core.AiAppFacade.Types;

public class AiFacade(Core.DataProcessor dp)
    : AiAppFacadeBase(multipleFunctionsAtOneResponse: false)
{
    private readonly string _content = string.Join('\n', dp.Lines);

    public override string GetConstraints() =>
        $"""
Analyze the Data File and define the path to the item User requests.

Each step defined in Data File starts with `+`.
Each item defined in Data File starts with `Item:`.

Provide ONLY REQUIRED STEPS to get to the item user requested.

Check the Functions Call History. Once the Functions Call History has all the steps to get to the item - Call the `Exit` function.

User will provdide the number which represents the item below:
{GetNumberedItems()}

Data File content is below:
{_content}
""";

    public override AppDescription GetDescription() =>
        [
            new()
            {
                Name = nameof(PrintPath),
                Description =
                    "This function prints the step you provide as input parameter. As an input provide ONLY 1 STEP of the whole path.",
                Parameters = [new() { Name = "stepName", Description = "string of 1 step" }],
            },
        ];

    public void PrintPath(string stepName)
    {
        Console.WriteLine($"{stepName}");
    }

    private string GetNumberedItems()
    {
        var itemsString = string.Empty;

        for (var i = 0; i < dp.Items.Count(); i++)
        {
            var item = dp.Items.ElementAt(i);
            itemsString = string.Concat(itemsString, $"[{i + 1}] - {item}\n");
        }

        return itemsString;
    }
}
