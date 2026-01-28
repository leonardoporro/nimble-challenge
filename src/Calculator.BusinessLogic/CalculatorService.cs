using Calculator.BusinessLogic.Serialization;
using Calculator.BusinessLogic.Validations;

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

    public double Resolve(string? numberListLine)
    {
        if (string.IsNullOrWhiteSpace(numberListLine))
        {
            return 0;
        }

        var numberList = Serializer.Deserialize(numberListLine);
        
        foreach (var validator in Validators)
        {
            validator.Validate(numberList);
        }

        return numberList.Sum();
    }
}