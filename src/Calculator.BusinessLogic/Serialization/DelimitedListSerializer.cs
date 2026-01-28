using System.Globalization;

namespace Calculator.BusinessLogic.Serialization;

public class DelimitedListSerializer : IListSerializer
{
    public string[] Delimiters { get; set; } = [","];

    public List<double> Deserialize(string serializedNumbers)
    {
        if (string.IsNullOrWhiteSpace(serializedNumbers))
        {
            return new List<double> { 0 };
        }

        var parts = serializedNumbers.Split(
            Delimiters,
            StringSplitOptions.None);

        var result = new List<double>(parts.Length);

        foreach (var part in parts)
        {
            if (string.IsNullOrWhiteSpace(part))
            {
                result.Add(0);
                continue;
            }

            if (double.TryParse(
                part,
                NumberStyles.Float | NumberStyles.AllowThousands,
                CultureInfo.InvariantCulture,
                out var value))
            {
                result.Add(value);
            }
            else
            { 
                result.Add(0);
            }
        }

        return result;
    }
}