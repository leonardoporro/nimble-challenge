

using Calculator.BusinessLogic.Validations.Exceptions;

namespace Calculator.BusinessLogic.Validations;

public class UpperCountValidator : IListValidator
{
    public int ExpectedCount { get; set; } = 2;

    public void Validate(List<double> numbers)
    {
       if (numbers.Count > ExpectedCount)
       {
           throw new ItemCountException(numbers.Count, ExpectedCount);
       }
    }
}