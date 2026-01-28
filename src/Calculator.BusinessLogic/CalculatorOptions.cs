namespace Calculator.BusinessLogic;

public class CalculatorOptions
{
    public string Operator { get; set; } = "+";

    public string AlternateDelimiter { get; set; } = "\n";

    public bool AllowNegativeNumbers { get; set; } = false;

    public double UpperBound { get; set; } = 1000;
}
