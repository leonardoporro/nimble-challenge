namespace Calculator.ConsoleApp.Services;

public sealed class MultilineInputReader : IInputReader
{
    public string Read()
    {
        var lines = new List<string>();

        while (true)
        {
            var line = Console.ReadLine();
            if (line is null || line.Length == 0)
                break;

            lines.Add(line);
        }

        return string.Join("\n", lines);
    }
}