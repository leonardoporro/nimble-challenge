using Calculator.BusinessLogic.Serialization;
using Calculator.BusinessLogic.Validations;
using static System.String;

namespace Calculator.BusinessLogic;

public class CalculatorService
{
    public CalculatorService(
        CalculatorOptions options,
        IListSerializer serializer,
        IEnumerable<IListValidator> validators)
    {
        Serializer = serializer;
        Validators = validators;
        Options = options;
    }

    public IListSerializer Serializer { get; }

    public IEnumerable<IListValidator> Validators { get; }

    public CalculatorOptions Options { get; }

    public double Resolve(string? numberListLine, out string explain)
    {
        var numbers = Serializer.Deserialize(numberListLine, out var parts);
 
        foreach (var validator in Validators)
        {
            validator.Validate(numbers);
        }

        var value = ApplyOperator(numbers, Options.Operator);

        explain = $"{Join(Options.Operator, parts)} => {Join(Options.Operator, numbers)} = {value}";

        return value;
    }

    public double Resolve(string? numberListLine) => Resolve(numberListLine, out _);

    static double ApplyOperator(IReadOnlyList<double> numbers, string op)
    {
        if (numbers.Count == 0)
            return 0;

        var result = numbers[0];

        for (var i = 1; i < numbers.Count; i++)
        {
            var current = numbers[i];

            switch (op)
            {
                case "+":
                    result += current;
                    break;

                case "*":
                    result *= current;
                    break;

                case "-":
                    result -= current;
                    break;

                case "/":
                    result /= current == 0 ? 1 : current;
                    break;

                default:
                    throw new InvalidOperationException(
                        $"Unsupported operator '{op}'");
            }
        }

        return result;
    }
}