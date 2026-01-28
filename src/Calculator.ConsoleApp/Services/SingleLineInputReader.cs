namespace Calculator.ConsoleApp.Services;

public sealed class SingleLineInputReader : IInputReader
{
    public string Read()
    {
        return Console.ReadLine() ?? string.Empty;
    }
}