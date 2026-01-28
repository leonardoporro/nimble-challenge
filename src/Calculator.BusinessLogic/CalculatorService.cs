using Calculator.BusinessLogic.Serialization;
using Calculator.BusinessLogic.Validations;

namespace Calculator.BusinessLogic;

public class CalculatorService
{
    readonly IListSerializer _serializer;
    readonly IEnumerable<IListValidator> _validators;
    
    public CalculatorService(
        IListSerializer serializer,
        IEnumerable<IListValidator> validators)
    {
        _serializer = serializer;
        _validators = validators;
    }

    public double Resolve(string? numberListLine)
    {
        if (string.IsNullOrWhiteSpace(numberListLine))
        {
            return 0;
        }

        var numberList = _serializer.Deserialize(numberListLine);
        
        foreach (var validator in _validators)
        {
            validator.Validate(numberList);
        }

        return numberList.Sum();
    }
}