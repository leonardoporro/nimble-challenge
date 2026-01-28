using Calculator.BusinessLogic;
using Microsoft.Extensions.Hosting;

namespace Calculator.ConsoleApp;

public sealed class CalculatorRunner : BackgroundService
{
    readonly CalculatorService _calculator;

    public CalculatorRunner(CalculatorService calculator)
    {
        _calculator = calculator; 
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        Console.WriteLine("Calculator Challenge");
        Console.WriteLine("Enter numbers separated by ,.");
        Console.WriteLine();

        while (!stoppingToken.IsCancellationRequested)
        {
            Console.Write("> ");
            var line = Console.ReadLine();
 
            try
            {
                var result = _calculator.Resolve(line); 
                Console.WriteLine(result);
            }
            catch (Exception ex)
            { 
                Console.WriteLine($"ERROR: {ex.Message}");
            }

            await Task.Yield(); 
        }
    }
}