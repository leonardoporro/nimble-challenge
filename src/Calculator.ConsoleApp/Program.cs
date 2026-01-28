using Calculator.BusinessLogic;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Calculator.ConsoleApp;

public static class Program
{
    public static async Task Main(string[] args)
    {
        using var host = Host.CreateDefaultBuilder(args)
            .ConfigureServices((context, services) =>
            {
                services.AddCalculator();
                services.AddHostedService<CalculatorRunner>();
            })
            .Build();

        await host.RunAsync();
    }
}