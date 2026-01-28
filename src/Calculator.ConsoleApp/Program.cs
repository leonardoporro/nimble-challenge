using Calculator.BusinessLogic;
using Calculator.ConsoleApp.Services;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Calculator.ConsoleApp;

public static class Program
{
    public static async Task Main(string[] args)
    {
        var options = CalculatorOptionsParser.FromArgs(args);

        using var host = Host.CreateDefaultBuilder(args)
            .ConfigureLogging(logging =>
            {
                logging.ClearProviders();
            })
            .ConfigureServices((context, services) =>
            {
                services.AddCalculator(options);
                services.AddSingleton<IInputReader, MultilineInputReader>();
                services.AddHostedService<CalculatorRunner>();
            })
            .Build();

        await host.RunAsync();
    }
}