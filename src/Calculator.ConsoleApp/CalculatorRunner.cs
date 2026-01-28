using Calculator.BusinessLogic;
using Calculator.ConsoleApp.Services;
using Microsoft.Extensions.Hosting;

namespace Calculator.ConsoleApp;

public sealed class CalculatorRunner : BackgroundService
{
    private readonly CalculatorService _calculator;
    private readonly IInputReader _reader;

    public CalculatorRunner(
        CalculatorService calculator,
        IInputReader reader)
    {
        _calculator = calculator;
        _reader = reader;
    }

    protected override async Task ExecuteAsync(
        CancellationToken stoppingToken)
    {
        Console.WriteLine("Calculator Challenge");
        Console.WriteLine("Enter numbers (Ctrl+C to exit)");
        Console.WriteLine();

        Console.CancelKeyPress += (_, e) =>
        {
            e.Cancel = true;
            Environment.Exit(0);
        };
 
        while (!stoppingToken.IsCancellationRequested)
        {
            var input = _reader.Read();

            if (string.IsNullOrWhiteSpace(input))
                continue;

            try
            {
                var result = _calculator.Resolve(input, out string formula);
                Console.WriteLine(result);
                Console.WriteLine(formula);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"ERROR: {ex.Message}");
            }

            Console.WriteLine();
            await Task.Yield();
        }

        Console.WriteLine("Ctrl+C detected, exiting...");
    }
}
