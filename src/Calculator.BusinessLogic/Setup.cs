using Calculator.BusinessLogic.Serialization;
using Calculator.BusinessLogic.Validations;
using Microsoft.Extensions.DependencyInjection;

namespace Calculator.BusinessLogic;

public static class Setup
{
    public static void AddCalculator(this IServiceCollection services, CalculatorOptions? options = null)
    {
        options ??= new();

        services.AddSingleton(options);
        services.AddSingleton<CalculatorService>();
        services.AddSingleton<IListSerializer>(new DelimitedListSerializer
        {
            Delimiters = [",", options.AlternateDelimiter],
            DefaultValue = options.Operator == "/" || options.Operator == "*" ? 1 : 0
        });

        if (options.AllowNegativeNumbers is false)
        {
            services.AddSingleton<IListValidator, NegativeNotAllowedValidator>();
        }

        services.AddSingleton<IListValidator>(new UpperBoundNormalizer
        {
            UpperBoundValue = options.UpperBound
        });
    }
}