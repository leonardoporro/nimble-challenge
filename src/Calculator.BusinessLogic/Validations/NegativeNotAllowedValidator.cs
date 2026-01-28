using Calculator.BusinessLogic.Validations.Exceptions;

namespace Calculator.BusinessLogic.Validations;

public class NegativeNotAllowedValidator : IListValidator
{
    public void Validate(List<double> numbers)
    {
        var negatives = new List<double>();

        foreach(var number in numbers)
        {
            if (number < 0)
                negatives.Add(number);
        }

        if (negatives.Count > 0)
            throw new NegativeNotAllowedException(negatives);
    }
}