using Calculator.BusinessLogic;
using System.Globalization;

namespace Calculator.ConsoleApp.Services;

public static class CalculatorOptionsParser
{
    public static CalculatorOptions FromArgs(string[] args)
    {
        var options = new CalculatorOptions();

        foreach (var arg in args)
        {
            if (arg.Equals("--no-negatives", StringComparison.OrdinalIgnoreCase))
            {
                options.AllowNegativeNumbers = false;
            }
            else if (arg.StartsWith("--upper-bound=", StringComparison.OrdinalIgnoreCase))
            {
                var value = arg.Substring("--upper-bound=".Length);

                if (double.TryParse(
                    value,
                    NumberStyles.Float,
                    CultureInfo.InvariantCulture,
                    out var upperBound))
                {
                    options.UpperBound = upperBound;
                }
            }
            else if (arg.StartsWith("--delimiter=", StringComparison.OrdinalIgnoreCase))
            {
                options.AlternateDelimiter =
                    arg.Substring("--delimiter=".Length);
            }
            else if (arg.StartsWith("--operator=", StringComparison.OrdinalIgnoreCase))
            {
                options.Operator =
                    arg.Substring("--operator=".Length);
            }
        }

        return options;
    }
}