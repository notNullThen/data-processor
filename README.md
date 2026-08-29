My take on Data File processing console .NET app which finds path to specified Item

## Features

- Shows the list of items.
- Defines exact path to item selected by user.
- Notifies about wrongly provided item selection and initiates input re-try.
- Throws friendly errors depending on the situation:
    - Not provided file path.
    - Wrong file path.
    - Corrupted file.

## How to run

1. Ensure you have installed .NET environment
2. Clone the project

```bash
git clone https://github.com/notNullThen/data-processor.git
```

3. Navigate the cloned directory
```bash
cd data-processor/
```

4. Run the application
```bash
dotnet run --project DataProcessor DataProcessor/TestData/Data.medium.txt
```