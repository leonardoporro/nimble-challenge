namespace Calculator.BusinessLogic.Validations;

public class UpperBoundNormalizer : IListValidator
{
    public double UpperBoundValue { get; set; } = 1000;

    public void Validate(List<double> numbers)
    {
        for(int i = 0; i < numbers.Count; i++) 
        {
            if (numbers[i] > UpperBoundValue)
            {
                numbers[i] = 0;
            }
        }
    }
}
