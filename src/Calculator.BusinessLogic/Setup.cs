using Calculator.BusinessLogic.Serialization;
using Calculator.BusinessLogic.Validations;
using Microsoft.Extensions.DependencyInjection;

namespace Calculator.BusinessLogic;

public static class Setup
{
    public static void AddCalculator(this IServiceCollection services)
    {
        services.AddSingleton<CalculatorService>();
        services.AddSingleton<IListSerializer, DelimitedListSerializer>();
        services.AddSingleton<IListValidator, UpperCountValidator>();
    }
}