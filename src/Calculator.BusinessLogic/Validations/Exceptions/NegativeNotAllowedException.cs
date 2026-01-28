using System.ComponentModel.DataAnnotations;

namespace Calculator.BusinessLogic.Validations.Exceptions;

public class NegativeNotAllowedException : ValidationException
{
    public NegativeNotAllowedException(IEnumerable<double> numbers) :
        base($"Negative numbers are not allowed: {string.Join(", ", numbers)}")
    {
    }
}
