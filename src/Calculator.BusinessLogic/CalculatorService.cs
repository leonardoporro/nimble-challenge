using Calculator.BusinessLogic.Serialization;
using Calculator.BusinessLogic.Validations;
using static System.String;

namespace Calculator.BusinessLogic;

public class CalculatorService
{
    public CalculatorService(
        IListSerializer serializer,
        IEnumerable<IListValidator> validators)
    {
        Serializer = serializer;
        Validators = validators;
    }

    public IListSerializer Serializer { get; }

    public IEnumerable<IListValidator> Validators { get; }

    public double Resolve(string? numberListLine, out string explain)
    {
        var numberList = Serializer.Deserialize(numberListLine, out var parts);
 
        foreach (var validator in Validators)
        {
            validator.Validate(numberList);
        }

        var op = "+";
        var value = numberList.Sum();

        explain = $"{Join(op, parts)} => {Join(op, numberList)} = {value}";
            
        return value;
    }

    public double Resolve(string? numberListLine) => Resolve(numberListLine, out _);
}