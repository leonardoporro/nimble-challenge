using System.ComponentModel.DataAnnotations;

namespace Calculator.BusinessLogic.Validations.Exceptions;

public class ItemCountException : ValidationException
{
    public ItemCountException(int count, int expected) :
        base($"The list contains {count} elements, but {expected} elements were expected.")
    {
    }
}
